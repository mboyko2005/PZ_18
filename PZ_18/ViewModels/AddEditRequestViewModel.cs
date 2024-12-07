using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using PZ_18.Data;
using PZ_18.Models;

namespace PZ_18.ViewModels
{
    /// <summary>
    /// ViewModel для окна добавления/редактирования заявки.
    /// </summary>
    public class AddEditRequestViewModel : INotifyPropertyChanged
    {
        private Request _currentRequest;

        /// <summary>
        /// Текущая заявка.
        /// </summary>
        public Request CurrentRequest
        {
            get => _currentRequest;
            set
            {
                _currentRequest = value;
                OnPropertyChanged(nameof(CurrentRequest));
            }
        }

        /// <summary>
        /// Список возможных статусов заявки.
        /// </summary>
        public ObservableCollection<string> StatusOptions { get; } = new ObservableCollection<string>
        {
            "Новая заявка",
            "В процессе ремонта",
            "Ожидание запчастей",
            "Готова к выдаче"
        };

        /// <summary>
        /// Команда для сохранения заявки.
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Конструктор ViewModel.
        /// </summary>
        /// <param name="request">Заявка, которую редактируем или добавляем.</param>
        public AddEditRequestViewModel(Request request = null)
        {
            if (request == null)
            {
                CurrentRequest = new Request
                {
                    StartDate = DateTime.Now,
                    RequestStatus = "Новая заявка"
                };
            }
            else
            {
                CurrentRequest = request;
            }

            SaveCommand = new RelayCommand(SaveRequest);
        }

        /// <summary>
        /// Логика сохранения заявки.
        /// </summary>
        private void SaveRequest(object obj)
        {
            using (var context = new CoreContext())
            {
                if (CurrentRequest.RequestID == 0)
                {
                    context.Requests.Add(CurrentRequest);
                }
                else
                {
                    context.Requests.Update(CurrentRequest);
                }
                context.SaveChanges();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
