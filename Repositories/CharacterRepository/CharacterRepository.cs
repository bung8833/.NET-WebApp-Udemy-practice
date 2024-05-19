using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Repositories.CharacterRepository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DataContext _dataContext;

        public CharacterRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        /// <summary>
        /// Add the specified character to the database.
        /// </summary>
        /// <param name="newCharacter"></param>
        /// <returns>The id of the new character in the database</returns>
        public async Task<int> AddCharacter(AddCharacterDto newCharacter)
        {
            Character character = new Character() {
                Name = newCharacter.Name,
                HitPoints = newCharacter.HitPoints,
                Strength = newCharacter.Strength,
                Defense = newCharacter.Defense,
                Intelligent = newCharacter.Intelligent,
                Class = newCharacter.Class,
            };

            var entry = _dataContext.Characters.Add(character);
            await _dataContext.SaveChangesAsync();
            return entry.Entity.Id;
        }


        /// <summary>
        /// Update the specified character in the database.
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns>
        /// The updated character in the database.
        /// Null if character not found.
        /// </returns>
        public async Task<Character?> UpdateCharacter(UpdateCharacterDto updateDto)
        {
            // check if character exists
            Character? dbCharacter = await _dataContext.Characters
                .FirstOrDefaultAsync(c => c.Id == updateDto.Id);
            if (dbCharacter is null) return null;

            // update the character using ChangeTracking
            _dataContext.Entry(dbCharacter).CurrentValues.SetValues(updateDto);
            
            await _dataContext.SaveChangesAsync();
            return dbCharacter;
        }
    }
}