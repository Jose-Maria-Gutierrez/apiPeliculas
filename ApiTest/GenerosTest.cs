using AutoMapper;
using introEntity.Controllers;
using introEntity.DTOs;
using introEntity.Entidades;
using introEntity.Repositorios;
using introEntity.UoW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;


namespace ApiTest
{
    public class GenerosTest
    {

        [Fact]
        public void GetOk()
        {
            Task<IEnumerable<Genero>> generos = Task.Run(() => { return Enumerable.Empty<Genero>(); }); //para comparar
            Mock<ILogger<GenerosController>> logger = new Mock<ILogger<GenerosController>>(); //mock de ILogger
            Mock<IGeneroRepository> generoRepository = new Mock<IGeneroRepository>(); //mock de repositorio
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>(); //mock del unit of work
            unitOfWork.Setup(a => a.generoRepository).Returns(generoRepository.Object); //que devuelva el mock dentro del uow
            generoRepository.Setup(a => a.getAll()).Returns(generos); //mock de la respuesta del metodo

            var controller = new GenerosController(null, unitOfWork.Object, logger.Object); //instancio el controller

            var res = controller.Get(); //pruebo el metodo GET

            Assert.IsType<Task<ActionResult<IEnumerable<Genero>>>>(res); //verifico si es del tipo deseado
        }

        [Fact]
        public void GetOkAuto()
        {
            Task<IEnumerable<Genero>> generos = Task.Run(() => { return Enumerable.Empty<Genero>(); }); //para comparar
            Mock<ILogger<GenerosController>> logger = new Mock<ILogger<GenerosController>>(); //mock de ILogger
            Mock<IGeneroRepository> generoRepository = new Mock<IGeneroRepository>(); //mock de repositorio
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>(); //mock del unit of work
            unitOfWork.Setup(a => a.generoRepository).Returns(generoRepository.Object); //que devuelva el mock dentro del uow
            generoRepository.Setup(a => a.getAll()).Returns(generos); //mock de la respuesta del metodo

            var controller = new GenerosController(null, unitOfWork.Object, logger.Object); //instancio el controller

            var res = controller.GetAutorizado(); //pruebo el metodo GET

            Assert.IsType<Task<ActionResult<IEnumerable<Genero>>>>(res); //verifico si es del tipo deseado
        }

        [Fact]
        public void PostOk()
        {
            Mock<ILogger<GenerosController>> logger = new Mock<ILogger<GenerosController>>(); //mock de ILogger
            Mock<IGeneroRepository> generoRepository = new Mock<IGeneroRepository>(); //mock de repositorio
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>(); //mock del unit of work

            generoRepository.Setup(g => g.add(It.IsAny<Genero>())).Verifiable();
            unitOfWork.Setup(a => a.generoRepository).Returns(generoRepository.Object);
            unitOfWork.Setup(a => a.saveChanges()).Verifiable();


            var config = new MapperConfiguration(c => { c.CreateMap<GeneroDTO, Genero>(); });
            var mapeo = config.CreateMapper(); //creo el mapper

            var controller = new GenerosController(mapeo, unitOfWork.Object, logger.Object); //instancio el controller

            GeneroDTO env = new GeneroDTO { Nombre = "adsad" };

            var res = controller.Post(env); //devuelve Task<ActionResult>

            res.Wait();
            generoRepository.Verify(x => x.add(It.IsAny<Genero>()), Times.Once());
            Assert.NotNull(res);
            //Assert.IsType<Task<ActionResult>>(res);
            Assert.IsAssignableFrom<OkResult>(res.Result); //OkResult solo 200OK OkObjectResult es 200 con contenido
        }

        [Fact]
        public void deleteOk()
        {
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
            Mock<ILogger<GenerosController>> logger = new Mock<ILogger<GenerosController>>();
            Mock<IGeneroRepository> generoRepository = new Mock<IGeneroRepository>();

            Genero gen = new Genero { Id = 3 };

            generoRepository.Setup(g => g.getById(It.IsAny<int>())).Returns(Task.Run(() => { return gen; }));
            generoRepository.Setup(g => g.delete(It.IsAny<Genero>())).Verifiable();
            unitOfWork.Setup(u => u.saveChanges()).Verifiable();
            unitOfWork.Setup(u => u.generoRepository).Returns(generoRepository.Object);

            var idEnviar = 3;

            var controller = new GenerosController(null, unitOfWork.Object, logger.Object);
            var res = controller.Delete(idEnviar);

            Assert.NotNull(res);
            Assert.IsAssignableFrom<OkResult>(res.Result);
        }
        [Fact]
        public void deleteBad()
        {
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
            Mock<ILogger<GenerosController>> logger = new Mock<ILogger<GenerosController>>();
            Mock<IGeneroRepository> generoRepository = new Mock<IGeneroRepository>();

            Genero gen = new Genero { Id = 3 };

            generoRepository.Setup(g => g.getById(It.IsAny<int>())).Returns(Task.Run(() => { return Task.FromResult<Genero>(null); }));
            generoRepository.Setup(g => g.delete(It.IsAny<Genero>())).Verifiable();
            unitOfWork.Setup(u => u.saveChanges()).Verifiable();
            unitOfWork.Setup(u => u.generoRepository).Returns(generoRepository.Object);

            var idEnviar = 3;

            var controller = new GenerosController(null, unitOfWork.Object, logger.Object);
            var res = controller.Delete(idEnviar);

            //Assert.NotNull(res);
            Assert.IsAssignableFrom<NotFoundResult>(res.Result);
        }

        [Fact]
        public void putOk()
        {
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
            Mock<ILogger<GenerosController>> logger = new Mock<ILogger<GenerosController>>();
            Mock<IGeneroRepository> generoRepository = new Mock<IGeneroRepository>();

            generoRepository.Setup(g => g.update(It.IsAny<Genero>())).Verifiable();
            unitOfWork.Setup(u => u.saveChanges()).Verifiable();
            unitOfWork.Setup(u => u.generoRepository).Returns(generoRepository.Object);

            var config = new MapperConfiguration(c => { c.CreateMap<GeneroDTO, Genero>(); });
            var mapeo = config.CreateMapper(); //creo el mapper

            var controller = new GenerosController(mapeo, unitOfWork.Object, logger.Object);

            int idEnviar = 3;
            var genEnviar = new GeneroDTO { Nombre = "musical"};

            var res = controller.Put(idEnviar,genEnviar);

            Assert.NotNull(res.Result);
            Assert.IsAssignableFrom<OkResult>(res.Result);
        }

    }
}