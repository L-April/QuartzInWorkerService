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
        //                //ʹ�������򴴽��������
        //                //�����Quartz.NETע��һ��IJobFactoryͨ����DI�����л�ȡ������ҵ����ҵ����Scoped������ζ�����Ĺ�������ʹ�÷�Χ���񣬶��������ǵ����������ʱ����
        //                q.UseMicrosoftDependencyInjectionScopedJobFactory();

        //                #region δ��װ��
        //                //// Create a "key" for the job
        //                //var jobKey = new JobKey("HelloWorldJob");

        //                //// Register the job with the DI container
        //                //q.AddJob<HelloWorldJob>(opts => opts.WithIdentity(jobKey));

        //                //// Create a trigger for the job
        //                //q.AddTrigger(opts =>opts
        //                //    .ForJob(jobKey)
        //                //    .WithIdentity("HelloWorldJob-trigger")
        //                //    .WithCronSchedule("0/5 * * * * ?"));//ÿ5����ִ��һ�� 
        //                #endregion


        //                // Register the job, loading the schedule from configuration 
        //                q.AddJobAndTrigger<HelloWorldJob>(hostContext.Configuration);
        //            });
        //            // Add the Quartz.NET hosted service
        //            //������ر�ʱ�������ÿ�ȷ��Quartz.NET���˳�֮ǰ�ȴ���ҵ����������
        //            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        //            services.AddHostedService<Worker>();
        //        });



        //���NuGet����
        //Microsoft.Extensions.Hosting.Systemd������linuxϵͳ��
//Microsoft.Extensions.Hosting.WindowsServices������windowϵͳ��


        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            //�Ƿ���winƽ̨
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
                            //ʹ�������򴴽��������
                            //�����Quartz.NETע��һ��IJobFactoryͨ����DI�����л�ȡ������ҵ����ҵ����Scoped������ζ�����Ĺ�������ʹ�÷�Χ���񣬶��������ǵ����������ʱ����
                            q.UseMicrosoftDependencyInjectionScopedJobFactory();

                            #region δ��װ��
                            //// Create a "key" for the job
                            //var jobKey = new JobKey("HelloWorldJob");

                            //// Register the job with the DI container
                            //q.AddJob<HelloWorldJob>(opts => opts.WithIdentity(jobKey));

                            //// Create a trigger for the job
                            //q.AddTrigger(opts =>opts
                            //    .ForJob(jobKey)
                            //    .WithIdentity("HelloWorldJob-trigger")
                            //    .WithCronSchedule("0/5 * * * * ?"));//ÿ5����ִ��һ�� 
                            #endregion


                            // Register the job, loading the schedule from configuration 
                            q.AddJobAndTrigger<HelloWorldJob>(hostContext.Configuration);
                        });
                        // Add the Quartz.NET hosted service
                        //������ر�ʱ�������ÿ�ȷ��Quartz.NET���˳�֮ǰ�ȴ���ҵ����������
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
                        //ʹ�������򴴽��������
                        //�����Quartz.NETע��һ��IJobFactoryͨ����DI�����л�ȡ������ҵ����ҵ����Scoped������ζ�����Ĺ�������ʹ�÷�Χ���񣬶��������ǵ����������ʱ����
                        q.UseMicrosoftDependencyInjectionScopedJobFactory();

                        #region δ��װ��
                        //// Create a "key" for the job
                        //var jobKey = new JobKey("HelloWorldJob");

                        //// Register the job with the DI container
                        //q.AddJob<HelloWorldJob>(opts => opts.WithIdentity(jobKey));

                        //// Create a trigger for the job
                        //q.AddTrigger(opts =>opts
                        //    .ForJob(jobKey)
                        //    .WithIdentity("HelloWorldJob-trigger")
                        //    .WithCronSchedule("0/5 * * * * ?"));//ÿ5����ִ��һ�� 
                        #endregion


                        // Register the job, loading the schedule from configuration 
                        q.AddJobAndTrigger<HelloWorldJob>(hostContext.Configuration);
                    });
                    // Add the Quartz.NET hosted service
                    //������ر�ʱ�������ÿ�ȷ��Quartz.NET���˳�֮ǰ�ȴ���ҵ����������
                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

                    services.AddHostedService<Worker>();
                });

        }
    }
}
