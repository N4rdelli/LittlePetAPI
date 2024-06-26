﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LittlePetAPI.Data;
using LittlePetAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LittlePetAPI.Data;
using LittlePetAPI.Models;

namespace LittlePet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentosController : ControllerBase
    {
        private readonly MyContext _context;

        public AgendamentosController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Agendamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agendamento>>> GetAgendamentos()
        {
            if (_context.Agendamentos == null)
            {
                return NotFound();
            }
            return await _context.Agendamentos.ToListAsync();
        }

        // GET: api/Agendamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agendamento>> GetAgendamento(int id)
        {
            if (_context.Agendamentos == null)
            {
                return NotFound();
            }
            var agendamento = await _context.Agendamentos.FindAsync(id);

            if (agendamento == null)
            {
                return NotFound();
            }

            return agendamento;
        }

        // GET: api/Agendamentos/5
        [HttpGet("/PesquisaPor/Cliente/{cpf}")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAgendamentoByClientsName(string cpf)
        {
            if (_context.Agendamentos == null)
            {
                return NotFound();
            }

            var cliente = _context.Clientes.Where(c => c.CpfCliente == cpf).FirstOrDefault();
            if (cliente == null)
            {
                return BadRequest("Cliente não cadastrado.");
            }

            var listaPets = _context.Pets.Where(p => p.ClienteId == cliente.ClienteId).ToList();
            if (listaPets.Count == 0)
            {
                return BadRequest("Nenhum pet cadastrado para esse cliente.");
            }

            List<Agendamento> agendamentos = new List<Agendamento>();

            foreach (var pet in listaPets)
            {
                Agendamento agenda = (Agendamento)_context.Agendamentos.Where(a => a.PetId == pet.PetId);
                if (agenda != null)
                {
                    agendamentos.Add(agenda);
                }
            }

            if (agendamentos == null)
            {
                return NotFound("Não foi encontrado nenhum agendamento para esse cliente.");
            }

            return Ok(agendamentos);
        }

        // GET: api/Agendamentos/data
        [HttpGet("/PesquisaPor/Data/{date}")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAgendamentoByDateTime(DateTime date)
        {
            if (_context.Agendamentos == null)
            {
                return NotFound();
            }

            var agendamentos = await _context.Agendamentos.Where(a => a.DiaHoraAgendamento == date).ToListAsync();

            if (agendamentos == null)
            {
                return NotFound("Não existe nenhum agendamento para esse dia/horário.");
            }

            return Ok(agendamentos);
        }


        // PUT: api/Agendamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgendamento(int id, Agendamento agendamento)
        {
            if (id != agendamento.AgendamentoId)
            {
                return BadRequest();
            }

            _context.Entry(agendamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgendamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Agendamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Agendamento>> PostAgendamento(Agendamento agendamento)
        {
            if (_context.Agendamentos == null)
            {
                return NotFound();
            }

            if (agendamento.DiaHoraAgendamento <= DateTime.Now)
            {
                return BadRequest("Não é possível agendar um serviço para esse dia/horário.");
            }

            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgendamento", new { id = agendamento.AgendamentoId }, agendamento);
        }

        // DELETE: api/Agendamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgendamento(int id)
        {
            if (_context.Agendamentos == null)
            {
                return NotFound();
            }
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }

            _context.Agendamentos.Remove(agendamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AgendamentoExists(int id)
        {
            return (_context.Agendamentos?.Any(e => e.AgendamentoId == id)).GetValueOrDefault();
        }
    }
}
