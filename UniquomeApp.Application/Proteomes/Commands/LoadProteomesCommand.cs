using System.Reflection.Metadata;
using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Application.Mappings;
using UniquomeApp.Application.Services;
using UniquomeApp.Domain;
using UniquomeApp.Domain.Base;
using UniquomeApp.Utilities;

namespace UniquomeApp.Application.Proteomes.Commands;

public class LoadProteomesCommand : IRequest<long>, IMapTo<Proteome>
{
    public string FolderName { get; set; } = default!;
    public string Version { get; set; } = default!;
    internal class LoadProteomesHandler : IRequestHandler<LoadProteomesCommand, long>
    {
        private readonly IUniquomeExtendedRepository<Organism> _organismRepo;
        private readonly IUniquomeExtendedRepository<Proteome> _proteomeRepo;
        private readonly IUniquomeExtendedRepository<Uniquome> _uniquomeRepo;
        private readonly IUniquomeExtendedRepository<Protein> _proteinRepo;

        public LoadProteomesHandler(
            IUniquomeExtendedRepository<Proteome> proteomeRepo,
            IUniquomeExtendedRepository<Organism> organismRepo,
            IUniquomeExtendedRepository<Uniquome> uniquomeRepo,
            IUniquomeExtendedRepository<Protein> proteinRepo)
        {
            _proteomeRepo = proteomeRepo;
            _organismRepo = organismRepo;
            _uniquomeRepo = uniquomeRepo;
            _proteinRepo = proteinRepo;
        }

        public async Task<long> Handle(LoadProteomesCommand request, CancellationToken cancellationToken)
        {
            var newConsoleSub = new ConsoleSubscriber();
            var files = FolderUtilities.GetRecursiveDirectoryContents(request.FolderName);
            var fastaFiles = files.Where(x => x.EndsWith("fasta")).ToList();
            var uniquomeFiles = files.Where(x => x.EndsWith("uniquome")).ToList();
            foreach (var fastaFile in fastaFiles)
            {
                var organismName = Path.GetFileNameWithoutExtension(fastaFile);
                Console.WriteLine($"Importing Organism: {organismName}");
                var newOrganism = new Organism
                {
                    Name = organismName,
                    Description = organismName
                };
                var organism = await _organismRepo.AddAsync(newOrganism, cancellationToken);
                var newProteome = new Proteome
                {
                    Name = organismName,
                    Description = organismName,
                    Version = request.Version
                };
                var proteome = await _proteomeRepo.AddAsync(newProteome, cancellationToken);
                var proteins = FastaParser.GetProteinsFromFasta(fastaFile, newConsoleSub);
                foreach (var p in proteins)
                {
                    p.InProteomeId = proteome.Id;
                }

                await _proteinRepo.BulkInsert(proteins, 10000, cancellationToken);

                Console.WriteLine($"Fasta File: {fastaFile}");
                var uniquomeFile = $"{request.FolderName}/{Path.GetFileNameWithoutExtension(fastaFile)}.uniquome";
                if (File.Exists(uniquomeFile))
                {
                    Console.WriteLine("Uniquome OK");


                }
             
                else
                    Console.WriteLine("Uniquome Failed");

            }
            return 1;
        }
    }

}

public class ConsoleSubscriber : INotificationSubscriber
{

    public void SendMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void SendMessage(string message, bool inline)
    {
        if (inline)
            Console.Write($"{message}\r");
        else
            SendMessage(message);
    }
}