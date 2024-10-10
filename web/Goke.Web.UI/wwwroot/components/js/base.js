function findAncestor(element, selector) {
    try {
        while (element
            //&& (element instanceof Element)
            && !element.matches(selector)) {
            element = element.parentNode;
        }
        return element;
    } catch (error) {
        //console.error(error);
    }
}

function color_theme() {
    ///+
    const colorThemes = document.querySelectorAll('[name="themecolor"]');
    const themes = document.querySelectorAll('[name="theme"]');

    const prefersDarkScheme = window.matchMedia("(prefers-color-scheme: dark)");

    let color = localStorage.getItem("color");
    let theme = localStorage.getItem("theme");

    colorThemes.forEach((e) => {
        if (e.id === color) {
            e.checked = true;
        }
    });
    themes.forEach((themeElt) => {
        if (themeElt.id === theme) {
            themeElt.checked = true;
        }
    });

    if (color == null) {
        color = 'neutral';
    }

    if (theme == null) {
        if (prefersDarkScheme) {
            theme = "dark";
        } else {
            theme == "light";
        }
    }

    document.documentElement.className = `${color} ${theme}`;

    colorThemes.forEach((themeElt) => {
        themeElt.addEventListener('click', (e) => {

            color = themeElt.id;
            localStorage.setItem("color", color);
            document.documentElement.className = `${color} ${theme}`;
        });
    });

    themes.forEach((themeElt) => {
        themeElt.addEventListener('click', (e) => {

            theme = themeElt.id;
            localStorage.setItem("theme", theme);
            document.documentElement.className = `${color} ${theme}`;
        });
    });


    ///-
}

function navSystem(selector) {
    // Get all the anchor elements inside the navigation menu
    const navLinks = document.querySelectorAll(selector);

    // Loop through each anchor element
    navLinks.forEach(link => {
        link.addEventListener('click', (event) => {
            // Remove the 'active' class from all anchor elements
            navLinks.forEach(item => {
                if (item !== event.target) {
                    item.classList.remove('active');
                    item.classList.remove('active-child');
                }
            });

            // Add the 'active' class to the clicked anchor element
            // event.target.classList.add('active');
            const anchor = event.target.closest("a");   // Find closest Anchor (or self)
            if (!anchor) return;                        // Not found. Exit here.
            anchor.classList.add('active');

            // if it is sub-menu anchor,
            // add active to the ancestor anchor
            const ancestorDropdown = findAncestor(event.target, '.dropdown');
            if (ancestorDropdown) {
                // get first anchor
                const a = ancestorDropdown.querySelector('a')
                a.classList.add('active');
            }
        });
    });
}