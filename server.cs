using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using ZeroMQ;
namespace Examples
{
    static partial class Program
    {
        public static void Main(string[] args)
        {
            if (args == null || args.Length < 1) 
            {
                Console.WriteLine("Добро пожаловать на сервер.");
                Console.WriteLine();
            }
            using (var context = new ZContext()) 
            using (var responder = new ZSocket(context, ZSocketType.REP)) //создем сокеты которые будут принимать сообщения и отсылать ответы
            {
                responder.Bind("tcp://*:5555");
                while (true)
                {
                    using (ZFrame request = responder.ReceiveFrame()) //получаем строку с сообщением клиента
                    {
                        string receivedString = request.ReadString(); //получаем консоль
                        Console.WriteLine("Received {0}", receivedString);  //печатаем консоль
                        Thread.Sleep(1);
                        string a = Encoding.UTF8.GetBytes(receivedString).Length.ToString(); //Переводим строку в массив байт, получаем его длину и снова переводим в строку
                        responder.Send(new ZFrame(a)); //отсылаем ответ клиенту
                    }
                }
            }
        }
    }
}
