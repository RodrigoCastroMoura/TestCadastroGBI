using AutoMapper;
using GbiTestCadastro.Infra.Mappers.GbiTestCadastroProfile;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GbiTestCadastro.Test.Unit.Application.Usecases;

public abstract class UsecaseFixture
{
    protected IMapper _mapper;

    [TestInitialize]
    public virtual void TestInitialize()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.AddProfile<GbiTestCadastroProfile>();
        });

        _mapper = config.CreateMapper();
    }
}
