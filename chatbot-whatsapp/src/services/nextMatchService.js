const webScrapingGrpcService = require('../grpc/services/webScrapingGrpcService');

function checkIfMessageRequestsNextMatch(message) {
    try {
      if (message.toLowerCase() === "!proxima partida fluminense") {
        return getNextMatch("Fluminense");
      }
      if (message.toLowerCase() === "!proxima partida flamengo") {
        return getNextMatch("Flamengo");
      }
      if (message.toLowerCase() === "!proxima partida brusque") {
        return getNextMatch("Brusque");
      }
  
      return undefined;
    } catch (error) {
      return error;
    }
}

async function getNextMatch(team) {
  const client = await webScrapingGrpcService.getClientGrpc();
    return new Promise((resolve, reject) => {
      const request = {
        team: team 
      }
      client.GetNextMatch(request, (error, response) => {
        if (error) {
          reject("Ocorreu algum erro ao tentar obter a próxima partida: " + error);
        }
        resolve(response.nextMatch);
      });
    });
}

module.exports = {
  checkIfMessageRequestsNextMatch
}