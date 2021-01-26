

1. The Models folder open.
2. The Models => Boxer.cs class is created.

    public class Boxer
    {
        public int Id {get; set; }
        public string FullName {get; set; }
        public string Alias ​​{get; set; }
        public string Status {get; set; }
        public string Division {get; set; }
    }

3. The DataAccess folder opens.
4. DataAccess => SeedData folder opens. => The SeedBoxers.cs class opens.
    4.1. The Microsoft.EntityFrameworkCore.SqlServer package is installed.

    public class SeedBoxers: IEntityTypeConfiguration <Boxer>
    {
        public void Configure (EntityTypeBuilder <Boxer> builder)
        {
            builder.HasData (
                new Boxer {Id = 1, FullName = "Muhammed Ali", Alias ​​= "King In the Ring", Division = "Heavyweight", Status = "Retired"},
                new Boxer {Id = 2, FullName = "Mike Tyson", Alias ​​= "Iron", Division = "Heavyweight", Status = "Retired"},
                new Boxer {Id = 3, FullName = "Lenox Lewis", Alias ​​= "Lioness", Division = "Heavyweight", Status = "Retired"},
                new Boxer {Id = 4, FullName = "Evander Holyfiled", Alias ​​= "Real Deal", Division = "Heavyweight", Status = "Retired"},
                new Boxer {Id = 5, FullName = "Rock Marciano", Alias ​​= "Savage", Division = "Heavyweight", Status = "Retired"});
        }
    }
5. DataAccess => Context => ApplicationDbContext.cs class opens.

    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext (DbContextOptions <ApplicationDbContext> options): base (options) {}

        public DbSet <Boxer> Boxers {get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration (new SeedBoxers ());
            base.OnModelCreating (modelBuilder);
        }
    }

6. The "ApplicationDbContext.cs" class, which will cause dependencies on other classes due to the Repository pattern, is registered in the built-in IoC Container into the COnfigure method in the Startup.cs class.

    services.AddDbContext <ApplicationDbContext> (options => options.UseSqlServer (Configuration.GetConnectionString ("DefaultConnection")));

7. Write connextion string to be called from appsetting.json file.

  "ConnectionStrings": {
    "DefaultConnection": "Server = (localdb) \\ mssqllocaldb; Database = YMS5177_API_CRUD_Db; Trusted_Connection = True;"
  }

8. Migration is done.
    8.1. Microsoft.EntityFrameworkCore.Tools

9. DataAccess => Repositories folder opens => IRepository.cs opens.

    public interface IRepository
    {
        ICollection <Boxer> GetBoxer ();
        Boxer GetBoxer (int id);
        bool IsBoxerExsist (string fullName);
        bool IsBoxerExsist (int id);

        bool Create (Boxer entity);
        bool Update (Boxer entity);
        bool Delete (Boxer entity);
        bool Save ();
    }

10. DataAccess => Repositories => Repository.cs class opens.

    public class Repository: IRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public Repository (ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;


        public bool Create (Boxer entity)
        {
            _applicationDbContext.Add (entity);
            return Save ();
        }

        public bool Delete (Boxer entity)
        {
            _applicationDbContext.Remove (entity);
            return Save ();
        }

        public ICollection <Boxer> GetBoxer () => _applicationDbContext.Boxers.ToList ();

        public Boxer GetBoxer (int id) => _applicationDbContext.Boxers.FirstOrDefault (x => x.Id == id);

        public bool IsBoxerExsist (string fullName) => _applicationDbContext.Boxers.Any (x => x.FullName.ToLower (). Trim () == fullName.ToLower (). Trim ());

        public bool IsBoxerExsist (int id) => _applicationDbContext.Boxers.Any (x => x.Id == id);

        public bool Save () => _applicationDbContext.SaveChanges ()> 0? true: false;

        public bool Update (Boxer entity)
        {
            _applicationDbContext.Update (entity);
            return Save ();
        }
    }

11. Since my Repository.cs class will be dependent in BoxerController.cs class, we will register this dependent class in Built-in container. Also, in order to comply with the DIP principle from SOLID principles, we need to call the IRepository.cs class in the relevant controller. When we call the relevant interface, we need to restore the IRepository.cs and Repository.cs classes into the built-in container, so that it will stretch the Repository.cs class, which is Concrete, that is, we want to reverse the control.
    11.1. Go to Startop.cs => Configure ().
      
        services.AddScoped <IRepository, Repository> ();
12. AutoMapper Implemantation

    AutoMapper

    AutoMapper is a simple and small library designed to solve a complex problem. Basically, the task it undertakes is very straight forward, it is a structure we prefer to get rid of the code that maps one object to another. Writing such a matching code is quite laborious and tedious, for example we want to bring the category and product information we hold in 2 separate tables. Let's assume that we have created a data transfer object (DTO) to fulfill this process and we have implemented the features that will meet our needs here. Another process we need to do in this scenario is to write a Linq to Entitiy query and synchronize the fields we need with the fields in our DTO. The process may sound quite simple here, especially in the scenario we have given as an example, however, as the work we do grows, the work done in the code we will write one by one manually in the select section will be large simultaneously and this process will be quite laborious and the code readability will be lost, and the query intelligibility will be lost. In such cases, we can simplify our work by writing special Data Transfer Objects for processing and mapping these objects with our entities with the automap tool.

    Doc: https://docs.automapper.org/en/stable/index.html

    12.1. Models => Dtos folder opens. => BoxerDto.cs opens. Remember that we need to create our data transfer objects on a business basis. Since the project scope is narrow, we will only create a single dto.

    public class BoxerDto
    {
        public int Id {get; set; }
        [Required]
        public string FullName {get; set; }
        public string Alias ​​{get; set; }
        public string Status {get; set; }
        public string Division {get; set; }
    }

    12.2. Let's download the packages below to take advantage of the AutoMapper library.
        12.2.1. AutoMapper - Version: 10.1.1
        12.2.2. AutoMapper.Extensions.Microsoft.DependencyInjection - Version: 8.1.0

    12.3. DataAccess => Mapper calender opens. => BoxingMapper.cs class opens.

        public class BoxerMapper: Profile
        {
            public BoxerMapper () => CreateMap <Boxer, BoxerDto> (). ReverseMap ();
        }

    12.4. Since the class that AutoMapper is used in, BoxingMapper.cs class will be used in the required controller classes, this creates a dependency. We need to register this dependent class in the built-in container. Already when starting this process, the 3rd part library was downloaded from Nuget. Thanks to the AutoMapper library, we will easily register our class that is dependent on the Configure () method of the Startup.cs class, namely BoxerMapper.cs.

            services.AddAutoMapper (typeof (BoxerMapper));

13. Controllers => BoxerContoller.cs Empty API Controller opens. CRUD operations are executed for the Boxer entity.

14. SwachBuckle Implemantation

    SwachBuckle
    
    RestFul is a tool for developers to help design, build, document web services. It is an open source software framework supported by a broad ecosystem. Swagger helps developers in three main areas. These:

    1-API Development: While creating the API, the Swager tool is used to create an automatic open API document according to the code itself. This API description is embedded in the source code of a project. Alternatively, using Swagger Codegen, developers can separate the source code from the Open API document and generate client code and documentation directly.
    2-Interacting with APIs: By using the Swagger Codegen project, end users create client SDKs directly from Open APIs and reduce the requirements for client code received or generated by the client.
    3- Documentation for APIs: When an OpenApI document is defined for an API, the Swagger open source tool is used to directly interact with the API via the Swagger user interface. It provides direct connection to live APIs via an Html-based user interface. Client requests can be made directly on the Swagger UI.
    
    You can access the Swagger Tool via Nuget Package Manager or GitHub and integrate it into your project.
    Github Link: https://github.com/swagger-api/swagger-ui
    Nuget Package Name: Swashbuckle.AspNetCore
    Doc: https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio

    14.1. Install the following packages for Swager.UI and Swashbuckle implementations
        14.1.1. Swashbuckle.AspNetCore
        14.1.2. Swashbuckle.AspNetCore.SwaggerUI

    14.2. Let's add the following code block to the Configure () method in the Startup.cs class.

            services.AddSwaggerGen (options =>
            {
                options.SwaggerDoc ("Restful API", new OpenApiInfo ()
                {
                    Title = "Restful API",
                    Version = "V.1",
                    Description = "Restful API",
                    Contact = new OpenApiContact () (
                    Email = "nihatcanertug@gmail.com",
                    Name = "Nihatcan Ertuğ",
                    Url = new Uri ("https://github.com/nihatcanertug")
                     },
                     License = new OpenApiLicense ()
                     {
                         Name = "MIT Licance",
                         Url = new Uri ("https://github.com/nihatcanertug")
                     }
                 });
             });

     14.3. Add the Middleware pipeline below to have the Swager.UI appear automatically when the application is started.
        
             app.UseSwagger ();
             app.UseSwaggerUI (options =>
             {
                 options.SwaggerEndpoint ("/ swagger / Restful API / swagger.json", "Restful API");
             });

    
                 



        



