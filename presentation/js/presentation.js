class Presentation {
    constructor() {
        this.currentSlide = 0;
        this.totalSlides = 15;
        this.slides = null;
        this.init();
    }

    init() {
        // Wait for DOM elements to be available
        this.slides = document.querySelectorAll('.slide');
        if (this.slides.length === 0) {
            console.warn('No slides found. Make sure DOM is fully loaded.');
            return;
        }
        
        // Update total slides count based on actual slides found
        this.totalSlides = this.slides.length;
        console.log(`Found ${this.totalSlides} slides`);
        
        this.setupEventListeners();
        this.updateSlideCounter();
        this.updateNavigationButtons();
        
        // Show first slide
        if (this.slides[0]) {
            this.slides[0].classList.add('active');
        }
    }

    setupEventListeners() {
        // Check if elements exist before adding listeners
        const prevBtn = document.getElementById('prevBtn');
        const nextBtn = document.getElementById('nextBtn');
        const menuBtn = document.getElementById('menuBtn');
        
        if (prevBtn) {
            prevBtn.addEventListener('click', () => this.previousSlide());
        }
        
        if (nextBtn) {
            nextBtn.addEventListener('click', () => this.nextSlide());
        }
        
        if (menuBtn) {
            menuBtn.addEventListener('click', () => this.toggleMenu());
        }
        
        // Menu items
        const menuItems = document.querySelectorAll('.menu-item');
        menuItems.forEach(item => {
            item.addEventListener('click', (e) => {
                const slideIndex = parseInt(e.target.dataset.slide);
                this.goToSlide(slideIndex);
                this.toggleMenu();
            });
        });

        // Keyboard navigation
        document.addEventListener('keydown', (e) => {
            switch(e.key) {
                case 'ArrowLeft':
                case 'ArrowUp':
                    this.previousSlide();
                    break;
                case 'ArrowRight':
                case 'ArrowDown':
                case ' ':
                    this.nextSlide();
                    break;
                case 'Home':
                    this.goToSlide(0);
                    break;
                case 'End':
                    this.goToSlide(this.totalSlides - 1);
                    break;
                case 'Escape':
                    this.closeMenu();
                    break;
            }
        });

        // Close menu when clicking outside
        document.addEventListener('click', (e) => {
            const menu = document.getElementById('slideMenu');
            const menuBtn = document.getElementById('menuBtn');
            if (menu && menuBtn && !menu.contains(e.target) && !menuBtn.contains(e.target)) {
                this.closeMenu();
            }
        });
    }

    nextSlide() {
        if (this.currentSlide < this.totalSlides - 1) {
            this.goToSlide(this.currentSlide + 1);
        }
    }

    previousSlide() {
        if (this.currentSlide > 0) {
            this.goToSlide(this.currentSlide - 1);
        }
    }

    goToSlide(index) {
        if (index >= 0 && index < this.totalSlides && this.slides && this.slides.length > 0) {
            // Remove active class from current slide
            if (this.slides[this.currentSlide]) {
                this.slides[this.currentSlide].classList.remove('active');
            }
            
            // Update current slide index
            this.currentSlide = index;
            
            // Add active class to new slide
            if (this.slides[this.currentSlide]) {
                this.slides[this.currentSlide].classList.add('active');
            }
            
            // Update UI
            this.updateSlideCounter();
            this.updateNavigationButtons();
            
            // Trigger slide animations
            this.animateSlideContent();
        }
    }

    updateSlideCounter() {
        const counter = document.getElementById('slideCounter');
        if (counter) {
            counter.textContent = `${this.currentSlide + 1} / ${this.totalSlides}`;
        }
    }

    updateNavigationButtons() {
        const prevBtn = document.getElementById('prevBtn');
        const nextBtn = document.getElementById('nextBtn');
        
        if (prevBtn) {
            prevBtn.disabled = this.currentSlide === 0;
        }
        if (nextBtn) {
            nextBtn.disabled = this.currentSlide === this.totalSlides - 1;
        }
    }

    toggleMenu() {
        const menu = document.getElementById('slideMenu');
        if (menu) {
            menu.classList.toggle('active');
        }
    }

    closeMenu() {
        const menu = document.getElementById('slideMenu');
        if (menu) {
            menu.classList.remove('active');
        }
    }

    animateSlideContent() {
        if (this.slides && this.slides[this.currentSlide]) {
            const currentSlideContent = this.slides[this.currentSlide].querySelector('.slide-content');
            if (currentSlideContent) {
                currentSlideContent.style.animation = 'none';
                setTimeout(() => {
                    currentSlideContent.style.animation = 'slideIn 0.6s ease-out';
                }, 10);
            }
        }
    }
}

// Code syntax highlighting for code blocks
function highlightCode() {
    const codeBlocks = document.querySelectorAll('.code-block');
    codeBlocks.forEach(block => {
        if (typeof Prism !== 'undefined') {
            Prism.highlightElement(block);
        }
    });
}

// Initialize presentation when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    new Presentation();
    highlightCode();
});

// Utility functions for interactive elements
function showCodeExample(elementId) {
    const element = document.getElementById(elementId);
    if (element) {
        element.style.display = element.style.display === 'none' ? 'block' : 'none';
    }
}

function copyToClipboard(text) {
    navigator.clipboard.writeText(text).then(() => {
        // Show temporary feedback
        const feedback = document.createElement('div');
        feedback.textContent = 'Copied to clipboard!';
        feedback.style.cssText = `
            position: fixed;
            top: 20px;
            right: 20px;
            background: #10b981;
            color: white;
            padding: 10px 20px;
            border-radius: 8px;
            z-index: 9999;
            animation: fadeInOut 2s ease-in-out;
        `;
        document.body.appendChild(feedback);
        setTimeout(() => feedback.remove(), 2000);
    });
}

// Add CSS for copy feedback animation
const style = document.createElement('style');
style.textContent = `
    @keyframes fadeInOut {
        0% { opacity: 0; transform: translateY(-20px); }
        20% { opacity: 1; transform: translateY(0); }
        80% { opacity: 1; transform: translateY(0); }
        100% { opacity: 0; transform: translateY(-20px); }
    }
`;
document.head.appendChild(style);
