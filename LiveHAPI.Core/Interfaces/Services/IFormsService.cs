using System.Collections.Generic;
using LiveHAPI.Core.Model.QModel;
using LiveHAPI.Core.Model.Studio;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IFormsService
    {
        IEnumerable<Module> ReadModules();
        IEnumerable<Form> ReadForms();
        IEnumerable<FormImplementation> ReadFormImplementations();
        IEnumerable<FormProgram> ReadFormPrograms();
        IEnumerable<Concept> ReadConcepts();

        IEnumerable<Question> ReadQuestions();
        IEnumerable<QuestionBranch> QuestionBranches();
        IEnumerable<QuestionValidation> QuestionValidations();
        IEnumerable<QuestionReValidation> QuestionReValidations();
        IEnumerable<QuestionTransformation> QuestionTransformations();
        IEnumerable<QuestionRemoteTransformation> QuestionRemoteTransformations();
    }
}