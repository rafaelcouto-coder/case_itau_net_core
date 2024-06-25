﻿using CaseItau.Domain.Abstractions;
using MediatR;

namespace CaseItau.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
 where TQuery : IQuery<TResponse>
{
}