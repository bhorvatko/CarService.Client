using CarService.Client.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Model
{
    public class Warrant : RestorableObject
    {
        public event Action? OnExternalUpdate;

        private WarrantType warrantType = null!;
        private Step currentStep = null!;
        private ObservableCollection<Note> notes = new ObservableCollection<Note>();

        public int Id { get; set; }
        public DateTime DeadLine { get; set; }
        public Step CurrentStep { get => currentStep; set { if (value != null) SetProperty(ref currentStep, value); } }
        public bool IsUrgent { get; set; }
        public string Subject { get; set; } = string.Empty;
        public ObservableCollection<Note> Notes { get => notes; set => SetProperty(ref notes, value); }
        public WarrantType WarrantType
        {
            get => warrantType;
            set
            {
                if (warrantType == value) return;

                SetProperty(ref warrantType, value);

                if (warrantType != null && !warrantType.Steps.Contains(CurrentStep)) CurrentStep = warrantType.Steps.FirstOrDefault(s => s.Procedure.Id == CurrentStep?.Procedure.Id) ?? warrantType.Steps.First();
            }
        }

        public override bool Equals(object? obj)
        {
            return Id == (obj as Warrant)?.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public void NotifyExternalUpdateOccured()
        {
            OnExternalUpdate?.Invoke();
        }
    }
}
