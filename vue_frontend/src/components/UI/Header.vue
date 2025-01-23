<template>
    <header class="header">
      <nav class="navbar">
        <ul class="navbar-nav">
          <li class="nav-item"><a href="/bookbuddy/index">Bookbuddies</a></li>
          <li class="nav-item"><a href="/posts/index">Posts</a></li>
          <li class="nav-item"><a href="/bookbuddy/register">Register</a></li>
        </ul>
      </nav>
    </header>
  </template>
  
  <style scoped>
  .header {
    background-color: #007bff;
    padding: 1rem;
    color: white;
  }
  
  .navbar {
    display: flex;
    justify-content: space-between;
    align-items: center;
  }
  
  .navbar-brand a {
    color: white;
    font-size: 1.5rem;
    font-weight: bold;
    text-decoration: none;
  }
  
  .navbar-nav {
    list-style: none;
    display: flex;
    gap: 1rem;
  }
  
  .nav-item a {
    color: white;
    text-decoration: none;
    font-size: 1rem;
  }
  
  .nav-item a:hover {
    text-decoration: underline;
  }
  </style>