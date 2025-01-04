const puppeteer = require ('puppeteer');

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
	
	await delay(2500);
	
	FakePage.goto('[FAKEPAGEURL]');

    while(true) {
		FakePage.GoBack();
		await delay(2500);
		FakePage.GoForward();
		await delay(2500);
}})();
	