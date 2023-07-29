using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace CalendarMVCSite.Models
{
    public class CreateMeetingModel
    {
        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

    public class CreateMeetingModelValidator : AbstractValidator<CreateMeetingModel>
    {
        public CreateMeetingModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30).MinimumLength(2);
            RuleFor(x => x.StartDate).NotEmpty().GreaterThan(DateTime.Now);
            RuleFor(x => x.EndDate).NotEmpty().GreaterThan(DateTime.Now);
        }
    }
}
