using UniquomeApp.Domain;

namespace UniquomeApp.Application.Services
{
    public static class FastaParser
    {
        public static IList<Protein> GetProteinsFromFasta(string filename, INotificationSubscriber subscriber)
        {
            var proteins = new List<Protein>();
            int lineNo = 0;
            try
            {
                //                subscriber?.SendMessage($"Parsing {filename}");
                Protein currentProtein = null;
                using (var file = new StreamReader(filename))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        if (string.IsNullOrEmpty(line)) continue;

                        //Start of new Entry in FASTA
                        if (line[0] == '>')
                        {
                            var tokens = line.Split('|');
                            if (tokens.Length >= 3)
                            {
                                currentProtein = new Protein();
                                var pos = tokens[2].IndexOf("OS=", StringComparison.Ordinal);
                                currentProtein.ProteinStatus = line.StartsWith(">sp")
                                    ? EnumProteinStatus.Reviewed
                                    : EnumProteinStatus.UnReviewed;
                                currentProtein.Code = tokens[1];
                                currentProtein.Name = pos > 1 ? tokens[2].Substring(0, pos - 1) : tokens[1];
                                //                                subscriber?.SendMessage($"Parsing Protein: {currentProtein.Code}");
                                var genePos = tokens[2].IndexOf("GN=", StringComparison.Ordinal);
                                var pePos = tokens[2].IndexOf("PE=", StringComparison.Ordinal);
                                var svPos = tokens[2].IndexOf("SV=", StringComparison.Ordinal);
                                if (genePos > 0 && pePos > 0)
                                    currentProtein.Gene = tokens[2].Substring(genePos + 3, tokens[2].Length - pePos - 4).Trim();
                                if (pePos > 0 && svPos > 0)
                                    currentProtein.ProteinExistence = (short)Convert.ToInt32(tokens[2].Substring(pePos + 3, tokens[2].Length - svPos - 3).Trim());
                                if (svPos > 0)
                                    currentProtein.SequenceVersion = (short)Convert.ToInt32(tokens[2].Substring(svPos + 3).Trim());

                                proteins.Add(currentProtein);
                            }
                        }
                        else
                        {
                            if (currentProtein != null) currentProtein.Sequence += line;
                        }
                    }
                }

                foreach (var protein in proteins)
                {
                    protein.SequenceLength = protein.Sequence.Length;
                    protein.AnalyzeAminoacidPositions();
                }
                return proteins;
            }
            catch (Exception e)
            {
                subscriber?.SendMessage($"Unable to parse fasta file. Error in line {lineNo}: {e.Message}");
                throw new Exception($"Unable to parse fasta file. Error in line {lineNo}: {e.Message}");
            }
        }
    }

    public interface INotificationSubscriber
    {
        void SendMessage(string message);
        void SendMessage(string message, bool inline);
    }
}
