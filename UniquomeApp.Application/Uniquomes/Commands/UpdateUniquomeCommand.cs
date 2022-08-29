﻿using Ardalis.Specification;
using AutoMapper;
using MediatR;
using NodaTime;
using UniquomeApp.Application.Common.Exceptions;
using UniquomeApp.Application.Mappings;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.Uniquomes.Commands;

public class UpdateUniquomeCommand: IRequest, IMapTo<Uniquome>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Version { get; set; } = default!;
    public Instant CreationDate { get; set; }
    internal class UpdateUniquomeHandler : IRequestHandler<UpdateUniquomeCommand>
    {
        private readonly IRepositoryBase<Uniquome> _repo;
        private readonly IMapper _mapper;

        public UpdateUniquomeHandler(IRepositoryBase<Uniquome> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUniquomeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException($"Could not locate Record with id: {request.Id}");
            entity = _mapper.Map(request, entity);
            await _repo.UpdateAsync(entity, cancellationToken);
            return Unit.Value;
        }
    }
}