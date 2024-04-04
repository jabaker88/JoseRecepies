﻿using PersonalPlayGround.ClientInfo;
using PersonalPlayGround.RecepieData;
using PersonalPlayGround.RecepieReviewData;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace PersonalPlayGround
{
    public static class DIResolver
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Register services and interfaces

            // Recepie
            container.RegisterType<IRecepieRepository, RecepieRepository>();
            container.RegisterType<IRecepieService, RecepieService>();

            // RecepieReviews
            container.RegisterType<IRecepieReviewRepository, RecepieReviewRepository>();
            container.RegisterType<IRecepieReviewService,  RecepieReviewService>();

            // Client
            container.RegisterType<IClientRepository,  ClientRepository>();
            container.RegisterType<IClientService,  ClientService>();

            // set resolver for MVC Controllers
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}