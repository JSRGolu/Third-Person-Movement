# Unity Project: Player Movement with New Input System

This repository contains a Unity project showcasing basic player movement in two different scenes using Unity's New Input System. Each scene demonstrates different movement setups with distinct approaches to character control and camera management.

<h2>Features</h2>
<ul>
  <li>
    <b>Scene 1 (Character_Controller): Character Controller Setup</b>
    <ul>
      <li>Player movement using Unity's Character Controller.</li>
      <li>Configured with Unity's New Input System for handling input events.</li>
      <li>Camera controlled via Cinemachine for smooth tracking of the player.</li>
    </ul>
  </li>
  <li>
    <b>Scene 2 (Rigid_Body): Rigidbody Setup</b>
    <ul>
      <li>Player movement using Unity's Rigidbody physics-based approach.</li>
      <li>Also uses Unity's New Input System for player inputs.</li>
      <li>Camera control via Cinemachine for smooth player tracking.</li>
    </ul>
  </li>
</ul>

<h2>Project Structure</h2>
<pre>
/Assets
   /Scenes
      - Character_Controller.unity        # Scene demonstrating player movement using Character Controller
      - Rigid_Body.unity                  # Scene demonstrating player movement using Rigidbody
   /Scripts
      - PlayerMovement.cs                 # Script controlling player movement in Scene 1
      - PlayerMovemntRB.cs                # Script controlling player movement in Scene 2
  /Input Map
      - CustomInputs.inputactions         # Input mapping for Unity's New Input System
      - CustomInputs.cs                   # Script created by Input Mapping
</pre>

<h2>Requirements</h2>
<ol>
  <li><b>Unity version 2023.2.1f</b></li>
  <li>
    <b>New Imput System</b>
    <ul>
      <li>To install, go to the Unity Package Manager and install "Input System".</li>
      <li>Set your project to use the New Input System under <b>Edit > Project Settings > Player > Active Input Handling</b> and choose <b>Input System Package (New)</b>.</li>
    </ul>
  </li>
  <li>
    <b>Cinemachine</b>
    <ul>
      <li>To install, go to Unity Package Manager and install <b>"Cinemachine"</b>.</li>
    </ul>
  </li>
</ol>

<h2>How to Run the Project</h2>
<ol>
  <li>Clone the repository.</li>
  <li>Open the project in Unity.</li>
  <li>Ensure the required packages (New Input System, Cinemachine) are installed.</li>
  <li>Open either Scene 1(Input Using CC.unity) or Scene 2(Input Using RB.unity) to explore different player movement setups.</li>
</ol>

<h3>Controls (for both scenes)</h3>
<ul>
  <li><b>W/A/S/D:</b> Move the player.</li>
  <li><b>Mouse:</b> Controls camera rotation via Cinemachine.</li>
</ul>
<br>
<br>
Feel free to explore both scenes to understand the different approaches to player movement and how the <b>New Input System</b> can be integrated into a Unity project.
