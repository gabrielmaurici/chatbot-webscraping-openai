const webScrapingGrpcService = require('../grpc/services/webScrapingGrpcService');

async function checkIfMessageRequestsNextMatch(message) {
    try {
      if (message.toLowerCase() === "!proxima partida fluminense") {
        return await getNextMatch("Fluminense");
      }
      if (message.toLowerCase() === "!proxima partida flamengo") {
        return await getNextMatch("Flamengo");
      }
      if (message.toLowerCase() === "!proxima partida brusque") {
        return await getNextMatch("Brusque");
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
          return;
        }
        resolve(response.nextMatch);
      });
    });
}

module.exports = {
  checkIfMessageRequestsNextMatch
}