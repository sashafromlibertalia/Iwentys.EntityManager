﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iwentys.EntityManager.DataAccess;
using Iwentys.EntityManager.WebApiDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iwentys.EntityManager.WebApi;

public static class GetSubjectsByGroupId
{
    public record Query(int GroupId) : IRequest<Response>;
    public record Response(IReadOnlyCollection<SubjectProfileDto> Subjects);

    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IwentysEntityManagerDbContext _context;
        private readonly IMapper _mapper;

        public Handler(IwentysEntityManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            List<SubjectProfileDto> result = await _context
                .GroupSubjects
                .Where(gs => gs.StudyGroupId == request.GroupId)
                .Select(gs => gs.Subject)
                .ProjectTo<SubjectProfileDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);

            return new Response(result);
        }
    }
}