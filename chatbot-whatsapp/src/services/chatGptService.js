const chatGptGrpcService = require('../grpc/services/chatGptGrpcService');

function checkIfMessageRequestsAskQuestionsIA(message) {
    try {
      if (message.startsWith("!IA")) {
        return AskQuestionsIA(message);
      }
      return undefined;
    } catch (error) {
      return error;
    }
}

async function AskQuestionsIA(ask) {
    const client = await chatGptGrpcService.getClientGrpc();
    return new Promise((resolve, reject) => {
      const request = {
        ask: ask 
      };
      client.AskQuestion(request, (error, response) => {
        if (error) {
          reject("Ocorreu algum erro ao tentar falar com a IA: " + error);
        }
        resolve(response.responseIA);
      });
    });
}

module.exports = {
  checkIfMessageRequestsAskQuestionsIA
}