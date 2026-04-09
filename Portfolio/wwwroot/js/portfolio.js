/**
 * portfolio.js — UI interactions for the portfolio site.
 * Handles: navbar scroll shrink, active nav link highlighting,
 *          intersection-observer animations, smooth scroll.
 */
(function () {
    'use strict';

    /* ── Navbar scroll state ───────────────────────────── */
    const nav = document.getElementById('mainNav');
    if (nav) {
        const onScroll = () => {
            nav.classList.toggle('scrolled', window.scrollY > 40);
        };
        window.addEventListener('scroll', onScroll, { passive: true });
        onScroll();
    }

    /* ── Intersection Observer — fade-in sections ──────── */
    const observer = new IntersectionObserver(
        (entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.classList.add('visible');
                    observer.unobserve(entry.target);
                }
            });
        },
        { threshold: 0.1 }
    );

    document.querySelectorAll(
        '.stat-card, .skill-card, .project-card, .edu-card, .ach-card, .exp-card, .ft-block, .nav-card'
    ).forEach(el => {
        el.classList.add('fade-in');
        observer.observe(el);
    });

    /* ── Active nav link on scroll ─────────────────────── */
    const sections = document.querySelectorAll('section[id]');
    const navLinks = document.querySelectorAll('#navbarContent .nav-link');

    if (sections.length && navLinks.length) {
        const sectionObserver = new IntersectionObserver(
            (entries) => {
                entries.forEach(entry => {
                    if (entry.isIntersecting) {
                        const id = '#' + entry.target.id;
                        navLinks.forEach(link => {
                            const href = link.getAttribute('href');
                            link.classList.toggle('active', href === id || href === '/' + id);
                        });
                    }
                });
            },
            { rootMargin: '-40% 0px -55% 0px' }
        );
        sections.forEach(s => sectionObserver.observe(s));
    }
})();
