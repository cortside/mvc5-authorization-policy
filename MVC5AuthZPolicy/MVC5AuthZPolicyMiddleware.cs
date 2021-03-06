﻿// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Owin;
using Owin;
using System.Threading.Tasks;

namespace Schaeflein.Community.MVC5AuthZPolicy
{
	public class MVC5AuthZPolicyMiddleware : OwinMiddleware
	{
		private AuthorizationService service;
		public MVC5AuthZPolicyMiddleware(OwinMiddleware next, AuthorizationService svc) : base(next)
		{
			service = svc;
		}

		public async override Task Invoke(IOwinContext context)
		{
			context.Environment.Add("MVC5AuthZPolicy", service);
			await this.Next.Invoke(context);
		}
	}

	public static class MVC5AuthZPolicyMiddlewareHandler
	{
		public static IAppBuilder UseMVC5AuthZPolicy(this IAppBuilder app, IAuthorizationService authSvc)
		{
			app.Use<MVC5AuthZPolicyMiddleware>(authSvc);
			return app;
		}

	}
}
