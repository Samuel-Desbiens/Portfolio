const puppeteer = require ('puppeteer');

const siteUrl = "http://fakesite:5554/"
const profUrl = "http://ctf3-cybersecurity.ddnsfree.com:8080/"

async function delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

//Cookie et Local Storage Setup
(async () => {
	const browser = await puppeteer.launch({
		userDataDir: ".user_data",
		headless:true,
		args:['--no-sandbox']
	});
	
	const HomePage = await browser.newPage();
	const FakePage = await browser.newPage();
	
	await HomePage.goto('http://userservice:5556/');
	
	await HomePage.type('#loginName','Boromir');
	await HomePage.type('#loginPassword','securepassword1234abcdefghijklmnopqrstuvwxyz');
	
	await HomePage.click('#submitLoginButton');
	
	await delay(10000);
	
	//Avant remise commenter et utiliser l'autre
	FakePage.goto(siteUrl);

	//Avant remise utiliser se goto
	//FakePage.goto(profUrl);
	
	await delay(10000);

    while(true) {
	FakePage.reload();
	await delay(2500);
}})();
