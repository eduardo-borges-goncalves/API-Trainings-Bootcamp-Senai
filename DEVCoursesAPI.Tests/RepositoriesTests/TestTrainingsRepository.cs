﻿using DEVCoursesAPI.Data.DTOs;
using DEVCoursesAPI.Data.DTOs.TrainingDTO;
using DEVCoursesAPI.Data.Models;
using DEVCoursesAPI.Repositories;

namespace DEVCoursesAPI.Tests.RepositoriesTests
{
    public class TestTrainingsRepository
    {
        private readonly Training training;
        private readonly Users user;

        public TestTrainingsRepository()
        {
            training = new Training()
            {
                Name = "Name",
                Summary = "Summary",
                Duration = 10,
                Instructor = "Instructor",
                Author = Guid.NewGuid(),
                Active = false
            };

            user = new Users()
            {
                Name = "teste", 
                Age = 18, 
                Email = "asdfa@gmail.com",
                CPF = 01110222555,
                Password = "7894sd5ff4",
            };
        }

        [Fact]
        public async void CreateTraining_ShouldReturnGuidWhenCreatingTraining()
        {
            // Arrange
            TrainingRepository repository = new(new TestCoursesDbContextFactory());

            // Act
            Guid result = await repository.CreateTraining(training);

            // Assert
            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfTrainings()
        {
            // Arrange
            TrainingRepository repository = new(new TestCoursesDbContextFactory());

            await repository.CreateTraining(training);

            // Act
            List<Training> result = await repository.GetAll();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnTrainingWhenValid()
        {
            // Arrange
            TrainingRepository repository = new(new TestCoursesDbContextFactory());

            Guid trainingId = await repository.CreateTraining(training);

            // Act
            Training? result = await repository.GetByIdAsync(trainingId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(trainingId, result.Id);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnNullWhenInvalid()
        {
            // Arrange
            TrainingRepository repository = new(new TestCoursesDbContextFactory());

            Guid invalidId = Guid.NewGuid();

            // Act
            Training? result = await repository.GetByIdAsync(invalidId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void SuspendAsync_ShouldSuspendActiveTraining()
        {
            // Arrange
            TrainingRepository repository = new(new TestCoursesDbContextFactory());

            training.Active = true;
            Guid trainingId = await repository.CreateTraining(training);

            // Act
            bool result = await repository.SuspendAsync(trainingId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void SuspendAsync_ShouldNotSuspendInactiveTraining()
        {
            // Arrange
            TrainingRepository repository = new(new TestCoursesDbContextFactory());

            training.Active = false;
            Guid trainingId = await repository.CreateTraining(training);

            // Act
            bool result = await repository.SuspendAsync(trainingId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void CreateTrainingRegistration_ShouldReturnTrueWhenCreatingTrainingRegistration()
        {
            // Arrange
            TrainingRepository repository = new(new TestCoursesDbContextFactory());
            training.Active = true;
            Guid trainingId = await repository.CreateTraining(training);
            TrainingRegistrationDto trainingRegistrationDto = new TrainingRegistrationDto();
            trainingRegistrationDto.TrainingId = trainingId;
            trainingRegistrationDto.UserId = Guid.NewGuid();
            trainingRegistrationDto.TopicIds = new List<Guid>();
            trainingRegistrationDto.TopicIds.Prepend(Guid.NewGuid());

            // Act
            bool result = await repository.CreateTrainingRegistration(trainingRegistrationDto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void CreateTrainingRegistration_ShouldReturnFalseWhenCreatingTrainingRegistration()
        {
            // Arrange
            TrainingRepository repository = new(new TestCoursesDbContextFactory());
            Guid trainingId = await repository.CreateTraining(training);
            TrainingRegistrationDto trainingRegistrationDto = new TrainingRegistrationDto();
            trainingRegistrationDto.TrainingId = trainingId;
            trainingRegistrationDto.UserId = Guid.NewGuid();
            trainingRegistrationDto.TopicIds = new List<Guid>();
            trainingRegistrationDto.TopicIds.Prepend(Guid.NewGuid());

            // Act
            bool result = await repository.CreateTrainingRegistration(trainingRegistrationDto);

            // Assert
            Assert.False(result);
        }


        [Fact]
        public async void GetUsersRegisteredInTraining_ShouldReturnRegisteredUsers()
        {
            //Arrange
            TestCoursesDbContextFactory dbFactory = new ();
            UsersRepository usersRepository = new(dbFactory);
            TrainingRepository repository = new(dbFactory);

            Users user = new() { Email = "", Name = "", Password = ""};
            Guid userId = usersRepository.Add(user);

            training.Active = true;
            Guid trainingId = await repository.CreateTraining(training);          

            TrainingRegistrationDto registrationDto = new() { TrainingId = trainingId, UserId = userId, TopicIds = new List<Guid>() };
            await repository.CreateTrainingRegistration(registrationDto);

            // Act
            RegisteredUsers registeredUsers = await repository.GetUsersRegisteredInTraining(trainingId);
            int result = registeredUsers.ActiveUsers.Count;

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async void GetReports_ShouldReturnListOfReports()
        {
            // Arrange
            TrainingRepository repository = new(new TestCoursesDbContextFactory());

            await repository.CreateTraining(training);

            // Act
            List<TrainingReport> reports = await repository.GetReports();

            // Assert
            Assert.Single(reports);
        }

        [Fact]
        public async void GetTopics_ShouldReturnTopicsList()
        {
            TrainingRepository repository = new(new TestCoursesDbContextFactory());

            Guid trainingId = await repository.CreateTraining(training);

            var listTopics = await repository.GetTopics(trainingId);

            Assert.IsType<List<Topic>>(listTopics);
        }

        [Fact]
        public async void GetFilteredTopicUsers_ShouldReturnFilteredTopicUserList()
        {
            // Arrange
            TrainingRepository repository = new(new TestCoursesDbContextFactory());
            UsersRepository userRepository = new(new TestCoursesDbContextFactory());

            Guid trainingId = await repository.CreateTraining(training);
            Guid userId = userRepository.Add(user);

            // Act
            var topics = await repository.GetTopics(trainingId);
            var topicsUser = await repository.GetFilteredTopicUsers(topics, userId);
            var topicsEqualTopicsUser = topics.Join(topicsUser,
                    topic => topic.Id,
                    topicUser => topicUser.TopicId,
                    (_, topicUser) => topicUser)
                .ToList();

            // Assert
            Assert.IsType<List<TopicUser>>(topicsUser);
            Assert.Equal(topics.Count, topicsUser.Count);
            Assert.Equal(topics.Count, topicsEqualTopicsUser.Count);
        }

        [Fact]
        public async void GetFilteredTopicUsers_ShouldNotReturnFilteredTopicUserList()
        {
            // Arrange
            TrainingRepository repository = new(new TestCoursesDbContextFactory());
            UsersRepository userRepository = new(new TestCoursesDbContextFactory());

            Guid trainingId = await repository.CreateTraining(training);
            Guid userId = Guid.NewGuid();

            // Act
            var topics = await repository.GetTopics(trainingId);
            var topicsUser = await repository.GetFilteredTopicUsers(topics, userId);

            // Assert
            Assert.Empty(topicsUser);
        }
    }
}
