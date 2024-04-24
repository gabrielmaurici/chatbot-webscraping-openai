const webScrapingGrpcService = require('../grpc/services/webScrapingGrpcService');

async function checkIfMessageRequestsLastMatch(message) {
    try {
        if (message.toLowerCase() === "!ultima partida fluminense") {
            return await getLastMatch("Fluminense");
        }
        if (message.toLowerCase() === "!ultima partida flamengo") {
            return await getLastMatch("Flamengo");
        }
        if (message.toLowerCase() === "!ultima partida brusque") {
            return await getLastMatch("Brusque");
        }
        return undefined;
    } catch(error){
        return error;
    }
}

async function getLastMatch(team) {
    const client = await webScrapingGrpcService.getClientGrpc();
    return new Promise((resolve, reject) => {
      const request = {
        team: team 
      }
      client.GetLastMatch(request, (error, response) => {
        if (error) {
            reject("Ocorreu algum erro ao obter a última partida: " + error);
            return;
        }
        resolve(response.lastMatch);
      });
    });
}

module.exports = {
    checkIfMessageRequestsLastMatch
}