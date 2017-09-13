using System;
using System.Collections.Generic;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.QModel;
using LiveHAPI.Core.Model.Studio;

namespace LiveHAPI.Core.Service
{
   public class FormsService:IFormsService
    {
        private readonly ILookupRepository _lookupRepository;

        public FormsService(ILookupRepository lookupRepository)
        {
            _lookupRepository = lookupRepository;
        }

        public IEnumerable<Module> ReadModules()
        {
            return _lookupRepository.ReadAll<Module>();
        }

        public IEnumerable<Form> ReadForms()
        {
            return _lookupRepository.ReadAll<Form>();
        }

        public IEnumerable<FormImplementation> ReadFormImplementations()
        {
            return _lookupRepository.ReadAll<FormImplementation>();
        }

        public IEnumerable<FormProgram> ReadFormPrograms()
        {
            return _lookupRepository.ReadAll<FormProgram>();
        }

        public IEnumerable<Concept> ReadConcepts()
        {
            return _lookupRepository.ReadAll<Concept>();
        }

        public IEnumerable<Question> Questions()
        {
            return _lookupRepository.ReadAll<Question>();
        }

        public IEnumerable<QuestionBranch> QuestionBranches()
        {
            return _lookupRepository.ReadAll<QuestionBranch>();
        }

        public IEnumerable<QuestionValidation> QuestionValidations()
        {
            return _lookupRepository.ReadAll<QuestionValidation>();
        }

        public IEnumerable<QuestionReValidation> QuestionReValidations()
        {
            return _lookupRepository.ReadAll<QuestionReValidation>();
        }

        public IEnumerable<QuestionTransformation> QuestionTransformations()
        {
            return _lookupRepository.ReadAll<QuestionTransformation>();
        }

        public IEnumerable<QuestionRemoteTransformation> QuestionRemoteTransformations()
        {
            return _lookupRepository.ReadAll<QuestionRemoteTransformation>();
        }
    }
}
