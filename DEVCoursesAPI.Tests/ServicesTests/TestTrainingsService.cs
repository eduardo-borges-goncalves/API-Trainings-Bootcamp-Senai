﻿using DEVCoursesAPI.Data.DTOs.ModuleDTO;
using DEVCoursesAPI.Data.DTOs.TrainingDTO;
using DEVCoursesAPI.Data.Models;
using DEVCoursesAPI.Repositories;
using DEVCoursesAPI.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVCoursesAPI.Tests.ServicesTests
{
    public class TestTrainingsService
    {
        private readonly Mock<ITrainingRepository> _trainingsRepository;
        private readonly Mock<IModulesService> _modulesService;
        private readonly TrainingService service;

        public TestTrainingsService()
        {
            _trainingsRepository = new Mock<ITrainingRepository>();
            _modulesService = new Mock<IModulesService>();

            service = new TrainingService(_trainingsRepository.Object, _modulesService.Object);
        }

        [Fact]
        public async void CreateTrainingAsync_ShouldCreateTraining()
        {
            // Arrange
            Guid trainingId = Guid.NewGuid();

            _trainingsRepository.Setup(repo => repo.CreateTraining(It.IsAny<Training>()))
                                .ReturnsAsync(trainingId);

            CreateTrainingDto trainingDto = new CreateTrainingDto();
            trainingDto.Modules = new List<CreateModuleDto>();

            // Act
            Guid result = await service.CreateTrainingAsync(trainingDto);

            // Assert
            Assert.Equal(trainingId, result);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfTrainings()
        {
            //Arrange
            List<Training> list = new() { new Training(), new Training() };

            _trainingsRepository.Setup(repo => repo.GetAll())
                                .ReturnsAsync(list);

            // Act
            List<Training> result = await service.GetAll();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnTrainingWhenValid()
        {
            // Arrange
            Guid trainingId = Guid.NewGuid();
            Training training = new()
            {
                Id = trainingId,
                Author = Guid.NewGuid(),
                Instructor = "Instructor",
                Modules = new List<Module>(),
                Name = "Name",
                Summary = "Summary"
            };

            _trainingsRepository.Setup(repo => repo.GetByIdAsync(trainingId))
                .ReturnsAsync(training);

            // Act
            ReadTrainingDto? trainingDto = await service.GetByIdAsync(trainingId);

            // Assert
            Assert.Equal(trainingId, trainingDto.Id);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnNullWhenInvalid()
        {
            // Arrang
            Guid invalidId = Guid.NewGuid();

            // Act
            ReadTrainingDto? trainingDto = await service.GetByIdAsync(invalidId);

            // Assert
            Assert.Null(trainingDto);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void SuspendAsync_ShouldReturnTrueOnlyWhenThereAreNoActiveStudents(bool noActiveStudent)
        {
            // Arrange
            Guid trainingId = Guid.NewGuid();

            _trainingsRepository.Setup(repo => repo.CheckForActiveStudents(trainingId))
                .ReturnsAsync(noActiveStudent);

            _trainingsRepository.Setup(repo => repo.SuspendAsync(trainingId))
                .ReturnsAsync(true);

            // Act
            bool result = await service.SuspendAsync(trainingId);

            // Assert
            Assert.Equal(noActiveStudent, result);
        }
    }
}
