﻿using BusManager.Model.Entities;
using BusManager.Model.Interfaces;
using BusManager.Repository;
using MongoDB.Bson;

namespace BusManager.Model.Services
{
    public class LineService : ILineService
    {
        public bool AddLine(Line line)
        {
            return LineRepository.AddLine(line);
        }

        public List<Line>? GetActiveLines()
        {
            return LineRepository.GetActiveLines();
        }

        public List<Line>? GetAllLines()
        {
            return LineRepository.GetAllLines();
        }

        public Line? GetLineById(ObjectId id)
        {
            return LineRepository.GetLineById(id);
        }

        public bool UpdateLine(Line line)
        {
            return LineRepository.UpdateLine(line);
        }

        public bool DeleteLine(Line line)
        {
            return LineRepository.DeleteLine(line);
        }

        public Line? GetLastLine()
        {
            return LineRepository.GetLastLine();
        }
    }
}
