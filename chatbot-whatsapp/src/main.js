const { Client, LocalAuth } = require('whatsapp-web.js');
const qrcode = require('qrcode-terminal');
const botMenuService = require('./services/botMenuService');
const webScrapingService = require('./grpc/services/webScrapingService');

const client = new Client({
    authStrategy: new LocalAuth(),
    webVersionCache: {
        type: 'remote',
        remotePath: 'https://raw.githubusercontent.com/wppconnect-team/wa-version/main/html/2.2409.0.html',
    }
});

client.once('ready', () => {
    console.log('Client is ready!');
});

client.on('qr', (qr) => {
    qrcode.generate(qr, {small: true});
});

client.on('message_create', message => {
    let botMenu = botMenuService.checkIfMessageRequestsBotMenu(message.body);
    if(botMenu)
        message.reply(botMenu);

    

    CheckAndGetLastMatchIfMessageContainsLast(message);
    CheckAndGetNextMatchIfMessageContainsNext(message);
});

function CheckAndGetNextMatchIfMessageContainsNext(message) {
    if (message.body.toLowerCase() === "!proxima partida fluminense") {
        getNextMatch(message, "Fluminense");
    }
    if (message.body.toLowerCase() === "!proxima partida flamengo") {
        getNextMatch(message, "Flamengo");
    }
    if (message.body.toLowerCase() === "!proxima partida brusque") {
        getNextMatch(message, "Brusque");
    }
}

function CheckAndGetLastMatchIfMessageContainsLast(message) {
    if (message.body.toLowerCase() === "!ultima partida fluminense") {
        getLastMatch(message, "Fluminense");
    }
    if (message.body.toLowerCase() === "!ultima partida flamengo") {
        getLastMatch(message, "Flamengo");
    }
    if (message.body.toLowerCase() === "!ultima partida brusque") {
        getLastMatch(message, "Brusque");
    }
}

async function getLastMatch(message, team) {
    try {
        let lastMatch = await webScrapingService.getLastMatch(team);
        message.reply(lastMatch)
        
    } catch (error) {
        message.reply("Ocorreu algum erro ao obter a última partida: " + error);
    }
}

async function getNextMatch(message, team) {
    try {
        let lastMatch = await webScrapingService.getNextMatch(team);
        message.reply(lastMatch)
        
    } catch (error) {
        message.reply("Ocorreu algum erro ao obter a última partida: " + error);
    }
}

client.initialize();
