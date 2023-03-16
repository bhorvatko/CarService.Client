using CarService.Client.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Model
{
    public class WarrantType : RestorableObject
    {
        private string name = string.Empty;

        public int Id { get; set; }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public ObservableCollection<Step> Steps { get; set; } = new ObservableCollection<Step>();

        public void AddStep(Procedure stepProcedure)
        {
            Transition? newTransition = null;

            if (Steps.Any())
            {
                newTransition = new Transition();
                Steps.Last().ForwardTransition = newTransition;
                newTransition.SourceStep = Steps.Last();
            }

            Steps.Add(new Step()
            {
                Procedure = stepProcedure,
                BackTransition = newTransition,
            });

            if (newTransition != null) newTransition.TargetStep = Steps.Last();
        }

        public void RemoveStep(Step step)
        {
            Step? backStep = step.BackTransition?.SourceStep;
            Step? forwardStep = step.ForwardTransition?.TargetStep;

            if (forwardStep != null)
            {
                if (backStep == null)
                {
                    forwardStep.BackTransition = null;
                }
                else
                {
                    backStep.ForwardTransition = forwardStep.BackTransition;
                    backStep.ForwardTransition!.SourceStep = backStep;
                }
            }

            Steps.Remove(step);

        }

        public override bool Equals(object? obj)
        {
            return this.Id == (obj as WarrantType)?.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
