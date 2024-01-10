using AutoMapper;
using SoftRobotics.DataAccess;
using SoftRobotics.Domain;
using SoftRobotics.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRobotics.Business
{
    public class RandomWordService
    {
        private readonly SoftRoboticsContext _context;
        private readonly IMapper _mapper;
        public RandomWordService()
        {
            _context = new SoftRoboticsContext();
        }
        public RandomWordService(IMapper mapper)
        {
            _context = new SoftRoboticsContext();
            _mapper = mapper;
        }

        public void GenerateWord()
        {
            for(int i = 0; i < 10; i++)
            {
                var word = GenerateRandomWord();
                if (!IsWordInDatabase(word))
                {
                    var wordDto = new RandomWordDto()
                    {
                        Word = word,
                        CountWord = word.Length
                    };
                    var wordModel = MapToEntity(wordDto);
                    _context.RandomWords.Add(wordModel);
                    _context.SaveChanges();
                }
                else
                {
                    i--;
                }
                
            }
        }

        private string GenerateRandomWord()
        {
            var random = new Random();
            int lenght = random.Next(3,51);
            var word = new StringBuilder();
            char nextChar;
            if (random.Next(0, 2) == 0)
            {
                nextChar = (char)random.Next('a', 'z' + 1);
            }
            else
            {
                nextChar = (char)random.Next('A', 'Z' + 1);
            }
            for (int i = 0; i < lenght; i++)
            {
                if (char.IsUpper(nextChar))
                {
                    nextChar = (char)random.Next('a', 'z' + 1);
                }
                else
                {
                    nextChar = (char)random.Next('A', 'Z' + 1);
                }
                word.Append(nextChar);
            }
            return word.ToString();
        }   

        private bool IsWordInDatabase(string word)
        {
            var words = _context.RandomWords.Select(MapToDto).ToList();
            foreach(var item in words)
            {
                if(item.Word == word)
                {
                    return true;
                }
            }
            return false;
        }
        public RandomWordDto? GetById(int id)
        {
            try
            {
                var wordsDto = _context.RandomWords.Select(MapToDto).FirstOrDefault(word => word.Id == id);
                return wordsDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }
        public IEnumerable<RandomWordDto> GetAll()
        {
            try
            {
                return _context.RandomWords.Select(MapToDto).ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<RandomWordDto>();
            }
        }
        public CommandResult Delete(RandomWordDto wordDto)
        {
            try
            {
                //var word = _mapper.Map<RandomWord>(wordDto);
                var word = MapToEntity(wordDto);
                _context.Remove(word);
                _context.SaveChanges();
                return CommandResult.Success("Silme işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Silme işlemi hatası !!", ex);
                throw;
            }
        }
        public CommandResult Delete(int id)
        {
            return Delete(new RandomWordDto() { Id = id });
        }


        private static RandomWord MapToEntity(RandomWordDto wordDto)
        {
            RandomWord entity = null;
            if (wordDto != null)
            {
                entity = new RandomWord()
                {
                    Id = wordDto.Id,
                    Word = wordDto.Word,
                    CountWord = wordDto.CountWord
                };
            }
            return entity;
        }
        private RandomWordDto MapToDto(RandomWord word)
        {
            RandomWordDto dto = null;
            if (word != null)
            {
                dto = new RandomWordDto()
                {
                    Id = word.Id,
                    Word = word.Word,
                    CountWord = word.CountWord
                };
            }
            return dto;
        }
    }
}
