// See https://aka.ms/new-console-template for more information
using System.Text;
using ParaPlus.Business.FileProcessing;
using ParaPlus.Business.Model;
using ParaPlus.Business.Presentations;
using ParaPlus.Console.Jobs;

IJob job1 = new IssuedInventorAwardsJob();


job1.ExecuteJob();

