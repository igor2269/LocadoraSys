﻿using LocadoraSys.Data;
using LocadoraSys.Data.DTOs;
using LocadoraSys.Model;
using Microsoft.EntityFrameworkCore;

namespace LocadoraSys.Services
{
    public class ClienteService
    {
        private readonly LocadoraSysContext _sysContext;

        public ClienteService(LocadoraSysContext sysContext)
        {
            _sysContext = sysContext;
        }

        public async Task<List<ClienteDto>> BuscaClientes()
        {
            try
            {
                return await _sysContext.Clientes.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na conexão com DB " + ex.Message); 
            }
        }

        public async Task<Cliente> BuscarClientePorId(int id)
        {
            try
            {
                var cliente = await _sysContext.Clientes.FirstOrDefaultAsync(c => c.Id == id);
                if(cliente == null)
                {
                    throw new Exception("Cliente não existe!");
                }
                return new Cliente
                {
                    Id = cliente.Id,
                    CPF = cliente.CPF,
                    Nome = cliente.Nome,
                    DataDeNascimento = cliente.DataNascimento
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na conexão com DB " + ex.Message);
            }
        }

        public async Task InserirCliente(Cliente cliente)
        {
            try
            {
                ClienteDto newClienteDto = new ClienteDto
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    CPF = cliente.CPF,
                    DataNascimento = cliente.DataDeNascimento
                };
                _sysContext.Clientes.Add(newClienteDto);
                await _sysContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na conexão com DB " + ex.Message);
            }
        }

        public async Task RemoveCliente(int id)
        {
            try
            {
                var cliente = _sysContext.Clientes.FirstOrDefault(x => x.Id == id);
                _sysContext.Clientes.Remove(cliente);
                await _sysContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na conexão com DB " + ex.Message);
            }
        }

        public async Task AtualizarCliente(Cliente clienteAtualizado)
        {
            try
            {
                var cliente = _sysContext.Clientes.FirstOrDefault(x => x.Id == clienteAtualizado.Id);
                if (cliente == null)
                {
                    throw new Exception("Cliente não existe!");
                }
                cliente.Nome = clienteAtualizado.Nome;
                cliente.CPF = clienteAtualizado.CPF;
                cliente.DataNascimento = clienteAtualizado.DataDeNascimento;

                _sysContext.Clientes.Update(cliente);
                await _sysContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Erro na conexão com DB " + ex.Message);
            }
        }
    }
}
