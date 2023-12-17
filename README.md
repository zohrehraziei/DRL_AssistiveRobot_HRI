## Application Deep Reinforcement Learning for Training Assistive Robot: Human-Robot Interaction

### Overview
This repository is dedicated to enhancing assistive robots through human-in-the-loop approaches, addressing the intersection of robotics and healthcare.

### Background and Challenges
- **Technical Limitations in Robotics**: Exploring the constraints faced by assistive robots, as discussed in Windrich et al. (2016).
- **Understanding Human Needs**: Investigating the gap in knowledge about human requirements in HRI, highlighted in Yan et al. (2015).
- **Safety in Reinforcement Learning**: Addressing safety concerns in applying RL to HRI, particularly in healthcare, following insights from Dalal et al. (2018).

### Project Objectives
- **Human-in-the-Loop Integration**: Implementing human feedback mechanisms in control systems of assistive robots, inspired by Christ et al. (2012).
- **Adaptability and Safety**: Focusing on environmental adaptability, particularly in response to patient movements.
- **Complex Goal-Oriented Tasks**: Utilizing deep RL algorithms to refine goal-oriented tasks in HRI.
- **Optimizing Reward Functions**: Aligning reward functions in RL with human preferences to optimize policy training.

### Research Highlights
- **Deep RL Architecture**: Implementing Proximal Policy Optimization (PPO) and Soft Actor-Critic (SAC) using the Unity ML-Agent plugin for non-stationary human-agent collaboration.
- **Robotic Simulation Environment**: Defined in Unity3D with Unity-ML-Agent, allowing for complex modeling of human-robot interactions. Refer to Bartneck et al. (2015) for further details.
- **Challenges and Previous Approaches**: Addressing the limitations of modeling complex behaviors in assistive robots, and comparing with existing solutions like the Assistive Gym toolkit.
![Digram for HRI Collboration](Diagrams-Human-Robot-Training.pdf)
### Research Assumptions
- **Robot as an Agent**: Training the robot for various positions correlating to human mouth reachability.
- **Human as a Non-Stationary Object**: Interaction dynamics include head rotation and Look-At-Face functions.
- **Safety and Operational Constraints**: Addressing collision avoidance, feeding requirements, and operational constraints in both Feeding and Drinking tasks.

### Reward Function Design
- **For Robot**: Combining distance-based penalties and rewards with trajectory-based bonuses for reaching target positions effectively.
- **For Human**: Dense reward function based on the proximity of the human mouth to the robot end-effector, considering head rotation and shared environmental space.

#### Note: 
Modeling the Sawyer robot asset, improving RL algorithms, and integrating realistic human feedback mechanisms pose significant challenges. While previous approaches have utilized frameworks like OpenAI Gym, our work focuses on scaling these concepts for more complex behaviors learned through human interaction.

### References
- Windrich, M., et al. (2016). "Active lower limb prosthetics: A systematic review of design issues and solutions." Biomedical Engineering Online.
- Yan, T., et al. (2015). "Review of assistive strategies in powered lower-limb orthoses and exoskeletons." Robotics and Autonomous Systems.
- Christ, O., et al. (2012). "The rubber hand illusion: Maintaining factors and a new perspective in rehabilitation and biomedical engineering?" Biomedical Engineering/Biomedizinische Technik.
- Dalal, G., et al. (2018). "Safe exploration in continuous action spaces." arXiv preprint arXiv:1801.08757.
- Bartneck, C., et al. (2015). "Robotics and Autonomous Systems." 

