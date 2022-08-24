
using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                //o que acontece aqui é:
                //esse método SeedAsync serve para inserir os dados automaticamente ao iniciar a app, a partir da classe Program.cs
                //No caso, eu passo o contexto nos parâmetros pra conseguir acessar o BD, e o logger pra gerar um log em caso de erro.
                //abaixo, eu checo se há algum valor dentro do DbSet ProductBrands, se não houver, eu vou ler o arquivo brands.json
                //e deserializar, e adicionar o que está no arquivo ao banco de dados =)
                //product brands
                if(!context.ProductBrands.Any())
                {
                    //tem que ser esse caminho porque o pro]eto inicial é API, que é onde vai ser lido o nosso Startup.cs/Program.cs
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach(var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                //product types
                if(!context.ProductTypes.Any())
                {
                    var productTypesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);
                    foreach(var item in productTypes)
                    {
                        context.ProductTypes.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                //products
                if(!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach(var item in products)
                    {
                        context.Products.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                //quando eu passo StoreContextSeed, é onde eu estou apontando onde o erro aconteceu.
                loggerFactory.CreateLogger<StoreContextSeed>().LogError(ex.Message, "Deu erro no StoreContextSeed");
            }
        }
    }
}