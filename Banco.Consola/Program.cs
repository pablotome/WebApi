using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Banco.Consola
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            var clientes = ProcessClientes().Result;

            foreach (var cliente in clientes)
                Console.WriteLine($"{ cliente.Apellido}, {cliente.Nombre}");
        }

        private static async Task<List<Cliente>> ProcessClientes()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            var streamTask = client.GetStreamAsync("http://localhost:64186/api/cliente");
            var serializer = new DataContractJsonSerializer(typeof(List<Cliente>));
            var clientes = serializer.ReadObject(await streamTask) as List<Cliente>;
            return clientes;
        }
    }
}