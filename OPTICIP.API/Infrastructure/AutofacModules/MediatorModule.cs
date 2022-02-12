using Autofac;
using MediatR;
using OPTICIP.API.Application.Commands.UsersCommands;
using System.Reflection;

namespace OPTICIP.API.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                   .AsImplementedInterfaces();

            // Enregistre toutes les classes Command (elles implémentent IRequestHandler) dans l'assemblage contenant les Command
            builder.RegisterAssemblyTypes(typeof(CreateUserCommand).GetTypeInfo().Assembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>));

            //// Enregistre toutes les classes Command (elles implémentent IRequestHandler) dans l'assemblage contenant les Command
            //builder.RegisterAssemblyTypes(typeof(IdentifiedCommand<,>).GetTypeInfo().Assembly)
            //       .AsClosedTypesOf(typeof(IRequestHandler<,>));

            ////// Enregistre toutes les classes DomainEventHandler ( elles implémentent INotificationHandler<>)  dans l'assemblage contenant les evenement de Domaines
            //builder.RegisterAssemblyTypes(typeof(PaysAddDomainEventHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(INotificationHandler<>));

            ////// Enregistre tous les Validateurs de Command (Validateurs basé sur la librairie FluentValidation)
            ////builder
            ////    .RegisterAssemblyTypes(typeof(CreerCompagnieCommandValidator).GetTypeInfo().Assembly)
            ////    .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            ////    .AsImplementedInterfaces();


            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

        }
    }
}
