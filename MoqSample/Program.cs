using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Mock<IHelloWorldDAL> mockedDAL = new Mock<IHelloWorldDAL>();
            string constId = "123";
            //var tst = mockedDAL.Setup(m => m.Fetch(new DataFetchRequest(constId))).Returns(new Helloworld { Id = constId });
            var tst = mockedDAL.Setup(m => m.Fetch(It.IsAny<DataFetchRequest>())).Returns(new Helloworld { Id = constId });
            mockedDAL.Setup(m => m.Get(It.IsAny<string>())).Returns(new Helloworld { Id = constId });

            var hw = mockedDAL.Object.Fetch(new DataFetchRequest(constId));
            hw = mockedDAL.Object.Get("13");

            if (hw != null)
            {
                Console.WriteLine(hw.Message);
            }

            Console.ReadKey();
        }
    }

    public class HelloWorldDAL : IHelloWorldDAL
    {
        public Helloworld Get(string hwid)
        {
            return new Helloworld
            {
                Id = hwid
            };
        }

        public Helloworld Fetch(DataFetchRequest request)
        {
            return new Helloworld
            {
                Id = request.Identifier,
                Message = string.Format(" you have requested {0}", request.Identifier)
            };
        }
    }

    public interface IHelloWorldDAL
    {
        Helloworld Get(string hwid);
        Helloworld Fetch(DataFetchRequest request);
    }

    public class Helloworld
    {
        public string Id { get; set; }
        public string Message { get; set; }
    }

    public class DataFetchRequest
    {
        public string Identifier { get; set; }

        public DataFetchRequest(string id)
        {
            Identifier = id;
        }
    }
}
