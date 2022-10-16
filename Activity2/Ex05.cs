//63010377_63010495_63010569_63010608_63010615_63010749

using System;
using System.Diagnostics;
using System.Threading;

namespace Ex05
{
	class Program
	{
		private static string x = "";
		private static int exitflag = 0;
		private static int updateFlag = 0;
		private static Object _Lock = new object();

		static void ThReadX(object i)
		{
			while (exitflag == 0)
				lock (_Lock)
				{
					if (x != "exit" && updateFlag == 1)
					{
						Console.WriteLine("***Thread {0} : x = {1}***", i, x);
					}
					updateFlag = 0;
				}
			Console.WriteLine("---Thread {0} exit---", i);
		}

		static void ThWriteX()
		{
			string ex;
			while (exitflag == 0)
				lock (_Lock)
				{
				Console.Write("Input: ");
				ex = Console.ReadLine();
				if (ex == "exit")
					exitflag = 1;
				x = ex;
				updateFlag = 1;
			}
		}
		static void Main(string[] args)
		{
			Thread A = new Thread(ThWriteX);
			Thread B = new Thread(ThReadX);
			Thread C = new Thread(ThReadX);
			Thread D = new Thread(ThReadX);

			A.Start();
			B.Start(1);
			C.Start(2);
			D.Start(3);
		}
	}
}
