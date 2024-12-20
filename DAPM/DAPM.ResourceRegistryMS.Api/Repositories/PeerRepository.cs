﻿using DAPM.ResourceRegistryMS.Api.Models;
using DAPM.ResourceRegistryMS.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAPM.ResourceRegistryMS.Api.Repositories
{
    public class PeerRepository : IPeerRepository
    {

        private readonly ILogger<IPeerRepository> _logger;
        private readonly ResourceRegistryDbContext _context;

        public PeerRepository(ILogger<IPeerRepository> logger, ResourceRegistryDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<Peer> AddPeer(Peer peer)
        {

            if (_context.Peers.Any(p => p.Id == peer.Id))
            {
                return peer;
            }

            await _context.Peers.AddAsync(peer);
            _context.SaveChanges();
            return peer;
        }

        public async Task<IEnumerable<Peer>> GetAllPeers()
        {
            return await _context.Peers.ToListAsync();
        }

        public async Task<Peer> GetPeerById(Guid id)
        {
            return await _context.Peers.FindAsync(id);
        }
    }
}
