using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads.Converters;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataPosition;
using BaseInsightDotNet.Core.Entities;
using BaseInsightDotNet.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.ImplementServices
{
    public class PositionService : IPositionService
    {
        private readonly IRepository<Position> _positionRepository;
        private readonly PositionConverter _positionConverter;
        public PositionService(IRepository<Position> positionRepository, PositionConverter positionConverter)
        {
            _positionRepository = positionRepository;
            _positionConverter = positionConverter;
        }
        public async Task<IQueryable<DataResponsePosition>> GetAllPositions()
        {
            var query = await _positionRepository.GetAllAsync();
            if (query == null) return null;
            return query.AsNoTracking().Select(item => _positionConverter.EntityToDTO(item));
        }
    }
}
