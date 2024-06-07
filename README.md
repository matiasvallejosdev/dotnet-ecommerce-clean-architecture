# Documentación de la Arquitectura en Capas

## Introducción
Esta documentación proporciona una visión detallada de la arquitectura en capas utilizada en el proyecto "dotnet-ecommerce-clean-architecture". La arquitectura se divide en cuatro capas principales: Servicios, Contratos, Controladores y Datos. Este enfoque promueve la separación de responsabilidades, mejora la mantenibilidad y facilita las pruebas.

## Descripción General de la Arquitectura
La aplicación está estructurada en los siguientes componentes clave, cada uno con responsabilidades específicas:

1. **Repositorios (Capa de Acceso a Datos)**
2. **Servicios (Capa de Lógica de Negocio)**
3. **Contratos (DTOs e Interfaces)**
4. **Controladores (Capa de Presentación)**

Cada componente interactúa con otros a través de interfaces bien definidas, lo que minimiza las dependencias directas y permite una mayor flexibilidad.

## Componentes Detallados

### Repositorios (Capa de Acceso a Datos)
**Propósito**: Manejar todas las interacciones con la fuente de datos, incluyendo bases de datos y APIs de terceros. Esta capa abstrae la lógica necesaria para acceder a estos datos.

**Responsabilidades**:
- Realizar operaciones CRUD (Create, Read, Update, Delete) en la base de datos.
- Gestionar conexiones a la base de datos.
- Ejecutar consultas y comandos SQL.
- Acceder a datos de APIs de terceros y procesarlos según sea necesario.

**Implementación**:
- Utilizar Entity Framework Core para interactuar con la base de datos, lo que reduce el código repetitivo y mejora la mantenibilidad.
- Definir interfaces de repositorios en la capa de Contratos para asegurar que las implementaciones estén desacopladas de sus usos.
- Implementar métodos para interactuar con APIs de terceros.

**Ejemplo**:
```csharp
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(int productId);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int productId);
    Task<IEnumerable<Product>> GetProductsFromExternalApiAsync();
}

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    // Otros métodos CRUD...

    public async Task<IEnumerable<Product>> GetProductsFromExternalApiAsync()
    {
        // Lógica para acceder a una API de terceros y obtener productos
    }
}
```

### Servicios (Capa de Lógica de Negocio)
**Propósito**: Encapsular la lógica de negocio de la aplicación. Los servicios operan sobre los datos obtenidos por los repositorios y los preparan para su presentación o procesamiento adicional.

**Responsabilidades**:
- Validar datos.
- Implementar reglas de negocio.
- Manejar lógica compleja y transformaciones de datos.

**Implementación**:
- Los servicios deben ser sin estado y centrarse únicamente en operaciones de negocio, separadas de la lógica de presentación.

**Ejemplo**:
```csharp
public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto> GetProductByIdAsync(int productId);
    Task AddProductAsync(ProductDto productDto);
    Task UpdateProductAsync(ProductDto productDto);
    Task DeleteProductAsync(int productId);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return products.Select(p => new ProductDto { Id = p.Id, Name = p.Name, Price = p.Price });
    }

    // Otros métodos de negocio...
}
```

### Contratos (DTOs e Interfaces)
**Propósito**: Definir las estructuras de datos para transferir datos entre capas y las interfaces que encapsulan las operaciones de servicios y repositorios.

**Responsabilidades**:
- Modelar los datos de acuerdo con las necesidades tanto de las operaciones internas como de las comunicaciones externas.
- Definir contratos claros para los servicios y repositorios.

**Implementación**:
- Utilizar DTOs para asegurar que los datos transferidos sean optimizados para los casos de uso específicos.

**Ejemplo**:
```csharp
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

### Controladores (Capa de Presentación)
**Propósito**: Gestionar las solicitudes y respuestas HTTP. Los controladores actúan como un puente entre la interfaz de usuario y la lógica de negocio de la aplicación.

**Responsabilidades**:
- Recibir y procesar las solicitudes HTTP.
- Llamar a los servicios correspondientes para manejar la lógica de negocio.
- Formatear y enviar las respuestas HTTP.

**Implementación**:
- Mantener los controladores ligeros y sin lógica de negocio. Solo deben manejar tareas específicas de la web.

**Ejemplo**:
```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    // Métodos adicionales para manejar las solicitudes de agregar, actualizar y eliminar productos
}
```

## Inyección de Dependencias (DI)
La inyección de dependencias es una parte integral de esta arquitectura, asegurando que las clases estén desacopladas y sean fácilmente testeables.

**Notas de Implementación de DI**:
- Configurar servicios, repositorios y otras dependencias en el archivo `Program.cs` utilizando el contenedor de DI.
- Inyectar dependencias a través de constructores para evitar el uso de instancias estáticas o singletons.
- Gestionar adecuadamente el ciclo de vida de las dependencias (transient, scoped, singleton).

**Ejemplo de Configuración**:
```csharp
var builder = WebApplication.CreateBuilder(args);

// Agregar servicios a la contenedor de DI
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configuración de middleware y endpoints...

app.Run();
```

## Mejores Prácticas
- **Separación de Preocupaciones**: Cada capa debe abordar solo las preocupaciones específicas relevantes a su funcionalidad.
- **Diseño Basado en Interfaces**: Programar con interfaces en lugar de implementaciones concretas para reducir dependencias y mejorar la modularidad.
- **Testabilidad**: Diseñar cada componente para que sea fácilmente testeable con mocks o stubs, facilitado por la inyección de dependencias.

## Ejemplo de Interacción entre Capas
1. **Manejo de Solicitudes HTTP**: Un controlador recibe una solicitud HTTP para obtener todos los productos.
2. **Operación del Controlador**: El controlador llama al método `GetAllProductsAsync` del servicio de productos.
3. **Procesamiento del Servicio**: El servicio llama al método `GetAllProductsAsync` del repositorio de productos para recuperar los datos.
4. **Obtención de Datos**: El repositorio obtiene los datos de la base de datos utilizando Entity Framework.
5. **Preparación de la Respuesta**: El servicio transforma los datos en DTOs y los devuelve al controlador.
6. **Envío de la Respuesta**: El controlador envía los DTOs como una respuesta HTTP al cliente.

## Conclusión
Esta arquitectura modular en capas promueve una clara separación de responsabilidades, permitiendo un desarrollo más organizado, mantenible y escalable. Al seguir estas directrices y mejores prácticas, tu equipo podrá desarrollar aplicaciones robustas y de alta calidad en .NET Core.
