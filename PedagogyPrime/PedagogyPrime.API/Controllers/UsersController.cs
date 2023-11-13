﻿using Microsoft.AspNetCore.Mvc;

namespace PedagogyPrime.API.Controllers
{
	using Infrastructure.Commands.Users.Create;
	using Infrastructure.Commands.Users.Delete;
	using Infrastructure.Commands.Users.Update;
	using Infrastructure.Models.User;
	using Infrastructure.Queries.Users.GetAll;
	using Infrastructure.Queries.Users.GetById;
	using MediatR;

	public class UsersController : BaseController
	{
		public UsersController(
			IMediator mediator
		) : base(mediator)
		{
		}

		[HttpGet]
		public async Task<ActionResult<List<UserDetails>>> GetAll()
		{
			return HandleResponse(await _mediator.Send(new GetAllUsersQuery()));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<UserDetails>> GetById(
			Guid id
		)
		{
			return HandleResponse(await _mediator.Send(new GetUserByIdQuery(id)));
		}

		[HttpPost]
		public async Task<ActionResult<bool>> Create(
			[FromBody] CreateUserCommand command
		)
		{
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<UserDetails>> Update(
			Guid id,
			[FromBody] UpdateUserCommand command
		)
		{
			command.Id = id;
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> Delete(
			Guid id
		)
		{
			return HandleResponse(await _mediator.Send(new DeleteUserCommand(id)));
		}
	}
}