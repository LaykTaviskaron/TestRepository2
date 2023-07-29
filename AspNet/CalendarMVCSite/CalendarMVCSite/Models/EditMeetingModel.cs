using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace CalendarMVCSite.Models
{
    public class EditMeetingModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

    public class EditMeetingModelValidator : AbstractValidator<EditMeetingModel>
    {
        public EditMeetingModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30).MinimumLength(2);
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
        }
    }
}
