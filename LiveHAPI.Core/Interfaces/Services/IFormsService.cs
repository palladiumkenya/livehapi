using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.QModel;
using LiveHAPI.Core.Model.Studio;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IFormsService
    {
        IEnumerable<Module> ReadModules();
        IEnumerable<Form> ReadForms();
        IEnumerable<FormImplementation> ReadFormImplementations();
        IEnumerable<FormProgram> ReadFormPrograms();
        IEnumerable<Concept> ReadConcepts();

        IEnumerable<Question> Questions();
        IEnumerable<QuestionBranch> QuestionBranches();
        IEnumerable<QuestionValidation> QuestionValidations();
        IEnumerable<QuestionReValidation> QuestionReValidations();
        IEnumerable<QuestionTransformation> QuestionTransformations();
        IEnumerable<QuestionRemoteTransformation> QuestionRemoteTransformations();
    }
}