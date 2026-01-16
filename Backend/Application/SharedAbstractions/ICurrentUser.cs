

using FormulaOne.Enums;

namespace FormulaOne.Application.SharedAbstractions
{
    public interface ICurrentUser
    {
        public Guid Id { get;  }
        public bool IsAuthenticated { get; }

        IReadOnlyCollection<string> Roles { get; }
        public bool IsInRole(Roles roles);
    }
}
