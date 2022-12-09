using testesapi.DTOs;
using testesapi.Exceptions;

namespace testesapi.Services
{
    public class ServiceService
    {
        public AOutput FuncaoA(AInput input)
        {
            if (input.Num < 0)
                throw new Exception("Deu Ruim");

            if (input.Txt.Equals("huehuehue brbr"))
                throw new TextoMalucoException("hu3");

            return new AOutput
            {
                Txt = $"{input.Txt} - {input.Num}"
            };
        }
    }
}