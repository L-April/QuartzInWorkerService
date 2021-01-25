using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using QuartzInWorkerService.Extensions;
using QuartzInWorkerService.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuartzInWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureServices((hostContext, services) =>
        //        {
        //            //Add the required Quartz.NET services
        //            services.AddQuartz(q =>
        //            {
        //                // Use a Scoped container to create jobs.
        //                //使用作用域创建这个容器
        //                //这告诉Quartz.NET注册一个IJobFactory通过从DI容器中获取创建作业的作业。该Scoped部分意味着您的工作可以使用范围服务，而不仅仅是单例服务或临时服务
        //                q.UseMicrosoftDependencyInjectionScopedJobFactory();

        //                #region 未封装的
        //                //// Create a "key" for the job
        //                //var jobKey = new JobKey("HelloWorldJob");

        //                //// Register the job with the DI container
        //                //q.AddJob<HelloWorldJob>(opts => opts.WithIdentity(jobKey));

        //                //// Create a trigger for the job
        //                //q.AddTrigger(opts =>opts
        //                //    .ForJob(jobKey)
        //                //    .WithIdentity("HelloWorldJob-trigger")
        //                //    .WithCronSchedule("0/5 * * * * ?"));//每5秒钟执行一次 
        //                #endregion


        //                // Register the job, loading the schedule from configuration 
        //                q.AddJobAndTrigger<HelloWorldJob>(hostContext.Configuration);
        //            });
        //            // Add the Quartz.NET hosted service
        //            //当请求关闭时，此设置可确保Quartz.NET在退出之前等待作业正常结束。
        //            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        //            services.AddHostedService<Worker>();
        //        });



        //添加NuGet包：
        //Microsoft.Extensions.Hosting.Systemd（部署linux系统）
//Microsoft.Extensions.Hosting.WindowsServices（部署window系统）


        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            //是否是win平台
            bool isWin = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            Console.WriteLine($"win:{isWin}");
            if (isWin)
            {
                return Host.CreateDefaultBuilder(args)
                    .UseWindowsService()//win
                    .ConfigureServices((hostContext, services) =>
                    {
                        //Add the required Quartz.NET services
                        services.AddQuartz(q =>
                        {
                            // Use a Scoped container to create jobs.
                            //使用作用域创建这个容器
                            //这告诉Quartz.NET注册一个IJobFactory通过从DI容器中获取创建作业的作业。该Scoped部分意味着您的工作可以使用范围服务，而不仅仅是单例服务或临时服务
                            q.UseMicrosoftDependencyInjectionScopedJobFactory();

                            #region 未封装的
                            //// Create a "key" for the job
                            //var jobKey = new JobKey("HelloWorldJob");

                            //// Register the job with the DI container
                            //q.AddJob<HelloWorldJob>(opts => opts.WithIdentity(jobKey));

                            //// Create a trigger for the job
                            //q.AddTrigger(opts =>opts
                            //    .ForJob(jobKey)
                            //    .WithIdentity("HelloWorldJob-trigger")
                            //    .WithCronSchedule("0/5 * * * * ?"));//每5秒钟执行一次 
                            #endregion


                            // Register the job, loading the schedule from configuration 
                            q.AddJobAndTrigger<HelloWorldJob>(hostContext.Configuration);
                        });
                        // Add the Quartz.NET hosted service
                        //当请求关闭时，此设置可确保Quartz.NET在退出之前等待作业正常结束。
                        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

                        services.AddHostedService<Worker>();
                    });
            }
            return Host.CreateDefaultBuilder(args)
                .UseSystemd()//linux
                .ConfigureServices((hostContext, services) =>
                {
                    //Add the required Quartz.NET services
                    services.AddQuartz(q =>
                    {
                        // Use a Scoped container to create jobs.
                        //使用作用域创建这个容器
                        //这告诉Quartz.NET注册一个IJobFactory通过从DI容器中获取创建作业的作业。该Scoped部分意味着您的工作可以使用范围服务，而不仅仅是单例服务或临时服务
                        q.UseMicrosoftDependencyInjectionScopedJobFactory();

                        #region 未封装的
                        //// Create a "key" for the job
                        //var jobKey = new JobKey("HelloWorldJob");

                        //// Register the job with the DI container
                        //q.AddJob<HelloWorldJob>(opts => opts.WithIdentity(jobKey));

                        //// Create a trigger for the job
                        //q.AddTrigger(opts =>opts
                        //    .ForJob(jobKey)
                        //    .WithIdentity("HelloWorldJob-trigger")
                        //    .WithCronSchedule("0/5 * * * * ?"));//每5秒钟执行一次 
                        #endregion


                        // Register the job, loading the schedule from configuration 
                        q.AddJobAndTrigger<HelloWorldJob>(hostContext.Configuration);
                    });
                    // Add the Quartz.NET hosted service
                    //当请求关闭时，此设置可确保Quartz.NET在退出之前等待作业正常结束。
                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

                    services.AddHostedService<Worker>();
                });

        }
    }
}
