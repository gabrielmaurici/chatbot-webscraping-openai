const webScrapingService = require('../grpc/services/webScrapingService');

async function checkIfMessageRequestsLastMatch(message) {
    try {
        if (message.toLowerCase() === "!ultima partida fluminense") {
            return getLastMatch("Fluminense");
        }
        if (message.toLowerCase() === "!ultima partida flamengo") {
            return getLastMatch("Flamengo");
        }
        if (message.toLowerCase() === "!ultima partida brusque") {
            return getLastMatch("Brusque");
        }
        return null;
    } catch(error){
        return error;
    }
}

async function getLastMatch(team) {
    const client = await webScrapingService.getClientGrpc();
    return new Promise((resolve, reject) => {
      const request = {
        team: team 
      }
      client.GetLastMatch(request, (error, response) => {
        if (error) {
            reject("Ocorreu algum erro ao obter a última partida: " + error);
        }
        resolve(response.lastMatch);
      });
    });
}

module.exports = {
    checkIfMessageRequestsLastMatch
}