    (function () {
      'use strict';
      const form = document.getElementById('registerForm');
      const password = document.getElementById('password');
      const confirmPassword = document.getElementById('confirmPassword');

      form.addEventListener('submit', function (event) {
        if (!form.checkValidity() || password.value !== confirmPassword.value) {
          event.preventDefault();
          event.stopPropagation();
          form.classList.add('was-validated');
          if (password.value !== confirmPassword.value) {
            confirmPassword.setCustomValidity("Mật khẩu không khớp");
          } else {
            confirmPassword.setCustomValidity("");
          }
        }
      }, false);
    })();