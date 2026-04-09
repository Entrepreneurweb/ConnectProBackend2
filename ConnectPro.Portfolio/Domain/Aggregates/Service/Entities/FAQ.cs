using ConnectPro.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Service.Entities
{
    public class FAQ : Entity<FAQ>
    {
        public Id<Service> ServiceId { get; private set; }
        private string _question;
        private string _answer;

        private FAQ() { }

        private FAQ(Guid serviceId, string question, string answer)
        {
            Id = Guid.NewGuid();
            ServiceId = serviceId;
            _question = question;
            _answer = answer;
        }

        public static FAQ Create(Guid serviceId, string question, string answer)
        {
            if (string.IsNullOrWhiteSpace(question)) throw new ArgumentException("Question is required.");
            if (string.IsNullOrWhiteSpace(answer)) throw new ArgumentException("Answer is required.");
            if (question.Length > 300) throw new ArgumentException("Question must not exceed 300 characters.");

            return new FAQ(serviceId, question.Trim(), answer.Trim());
        }

        public void UpdateQuestion(string question)
        {
            if (string.IsNullOrWhiteSpace(question)) throw new ArgumentException("Question is required.");
            if (question.Length > 300) throw new ArgumentException("Question must not exceed 300 characters.");
            _question = question.Trim();
        }

        public void UpdateAnswer(string answer)
        {
            if (string.IsNullOrWhiteSpace(answer)) throw new ArgumentException("Answer is required.");
            _answer = answer.Trim();
        }

        public string Question => _question;
        public string Answer => _answer;
    }
}
