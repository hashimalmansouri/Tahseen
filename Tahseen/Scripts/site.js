const home = document.querySelector('.bars');
const navbar = document.querySelector('.navbar ul');


function showNavbar() {
	home.addEventListener('click', () => {
		navbar.classList.toggle('fix-top');

	});
}
showNavbar();
