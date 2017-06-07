using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading; 
using ZeroMQ; 
namespace Examples 
{ 
    static partial class Program 
        { 
            public static void Main(string[] args) 
            { 
                using (var context = new ZContext()) 
                using (var requester = new ZSocket(context, ZSocketType.REQ)) 
                { 
                    requester.Connect("tcp://127.0.0.1:5555"); 
                    while (true) 
                        { 
                        Console.WriteLine("Отправь сообщение: \n"); 
                        string requestText = Console.ReadLine(); 

                        Console.WriteLine(); 
                        requester.Send(new ZFrame(requestText)); 
                        using (ZFrame reply = requester.ReceiveFrame()) 
                        { 
                            Console.WriteLine("Длина сообщения: " + reply.ReadString()); 
                        } 
                    } 
                } 
         } 
    } 
}
