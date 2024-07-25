using GbiTestCadastro.CrossCutting.Enums;
using GbiTestCadastro.Dto.GbiTestCadastro;

namespace GbiTestCadastro.Test.Shared.Dto
{
    public static class CreateGbiTestCadastroDefaultTestDto
    {
        public static GbiTestCadastroCreateDto GetDefault() =>
            new("Nalfu", GbiTestCadastroType.AWS);
    }
}

